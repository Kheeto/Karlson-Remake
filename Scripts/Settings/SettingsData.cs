using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData {

    public bool graphics;

    public SettingsData(bool graphics) {
        this.graphics = graphics;
    }
}
