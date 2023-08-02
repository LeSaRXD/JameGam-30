using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings {
    
    public static bool dead = false;
    public static bool paused = false;
    static float masterVolume = 1f;
    public static float MasterVolume {
        get {
            return masterVolume;
		}
        set {

            masterVolume = value;

            if(masterVolume > 1f) masterVolume = 1f;
            else if(masterVolume < 0f) masterVolume = 0f;
            
            AudioListener.volume = masterVolume;

		}
	}

    public static void Reset() {

        paused = false;
        dead = false;

	}

}
