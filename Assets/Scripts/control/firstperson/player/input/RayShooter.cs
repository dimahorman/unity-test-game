using System.Collections;
using UnityEngine;

namespace control.firstperson.player.input {
    public class RayShooter : PausableBehavior {
        private Camera _camera;
        private PlayerCharacter _playerCharacter;
    
        [SerializeField] private AudioSource soundSource; 
        [SerializeField] private AudioClip hitWallSound;
        [SerializeField] private AudioClip hitEnemySound;

        // Start is called before the first frame update
        void Start() {
            _camera = GetComponent<Camera>();
            _playerCharacter = GetComponentInParent<PlayerCharacter>();
        }

        // Update is called once per frame
        protected override void PausableUpdate() {
        
            if (Input.GetMouseButtonDown(0)) {
                // EventSystem.current.IsPointerOverGameObject()
                Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
                Ray ray = _camera.ScreenPointToRay(point);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    GameObject hitObject = hit.transform.gameObject;
                    ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                    // Debug.Log("Hit: " + hit.point);

                    if (target != null) {
                        Debug.Log("Target hit!");
                        target.ReactToHit();
                        soundSource.PlayOneShot(hitEnemySound);
                        GameEvent.HitEvent.Invoke();
                    }
                    else {
                        StartCoroutine(SphereIndicator(hit.point));
                        soundSource.PlayOneShot(hitWallSound);
                    }
                }
            }
        }

        private IEnumerator SphereIndicator(Vector3 pos) {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.position = pos;
            yield return new WaitForSeconds(1);
            Destroy(sphere);
        }
    }
}