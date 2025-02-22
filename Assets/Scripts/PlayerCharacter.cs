using UnityEngine;

public class PlayerCharacter : PausableBehavior {
    private int _health = 5;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    protected override void PausableUpdate() {

    }
    

    public void Hurt(int damage) {
        Managers.Player.ChangeHealth(-damage);
    }
}