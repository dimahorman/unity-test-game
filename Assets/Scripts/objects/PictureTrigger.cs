using UnityEngine;

public class PictureTrigger : BasePointAndClickDevice {
    public override void Operate() {
        Managers.Image.GetWebImage(SetImage);
    }

    private void SetImage(Texture2D image) {
        GetComponent<Renderer>().material.mainTexture = image;
    }
}