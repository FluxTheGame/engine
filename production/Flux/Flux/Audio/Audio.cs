using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Flux
{
    class Audio
    {
        private static Dictionary<string, string> audioFiles = new Dictionary<string, string>();
        public static FMOD.System system = null;


        //SOUND FX
        private static FMOD.Sound sound1 = null;
        private static FMOD.Sound sound2 = null;
        private static Dictionary<string, FMOD.Sound> sounds = new Dictionary<string, FMOD.Sound>();
        
        //MUSIC
        
        private static FMOD.Channel channel = null;

        public static void Initialize()
        {
            audioFiles.Add("ambient.birds", "sfx/ambient/birds.wav");
            audioFiles.Add("ambient.creek1", "sfx/ambient/creek1.wav");
            audioFiles.Add("ambient.creek2", "sfx/ambient/creek2.wav");
            audioFiles.Add("interactions.wind", "sfx/interactions/wind.wav");
            audioFiles.Add("interactions.leaves", "sfx/interactions/leaves_rustling.wav");
            audioFiles.Add("ambient.bass", "sfx/ambient/bass_event.wav");

            // constructor
            MultiSpeakerOutput_Load();
            Load(audioFiles);
        }

        public static void Dispose()
        {
            FMOD.RESULT result;

            if (sound1 != null)
            {
                result = sound1.release();
                ERRCHECK(result);
            }
            if (sound2 != null)
            {
                result = sound2.release();
                ERRCHECK(result);
            }
            if (system != null)
            {
                result = system.close();
                ERRCHECK(result);
                result = system.release();
                ERRCHECK(result);
            }
            //base.Dispose(disposing);
        }

        public static void Load(Dictionary<string, string> audioFileNames, bool loop = false)
        {
            FMOD.RESULT result;
            FMOD.Sound tmpSound;

            foreach (KeyValuePair<string, string> file in audioFileNames)
            {
                tmpSound = null;
                //LOAD IN PATHS OF AUDIO FILES
                string soundPath = "../../../../FluxContent/audio/" + file.Value;

                if (File.Exists(soundPath))
                {
                    //RUN FOR EACH SOUND FILE
                    result = system.createSound(soundPath, (FMOD.MODE.SOFTWARE | FMOD.MODE._2D), ref tmpSound);
                    ERRCHECK(result);
                    sounds[file.Key] = tmpSound;
                    FMOD.MODE loopAudio = (loop) ? FMOD.MODE.LOOP_NORMAL : FMOD.MODE.LOOP_OFF;
                    result = sounds[file.Key].setMode(FMOD.MODE.LOOP_OFF);
                    ERRCHECK(result);
                }
                else
                {
                    Console.WriteLine("FILE NOT FOUND: " + file.Value);
                }
            }
        }

        public static void MultiSpeakerOutput_Load()
        {

            uint version = 0;

            FMOD.RESULT result;

            result = FMOD.Factory.System_Create(ref system);
            ERRCHECK(result);

            result = system.getVersion(ref version);
            ERRCHECK(result);

            if (version < FMOD.VERSION.number)
            {
                Console.WriteLine("You are using an old version of FMOD FIX DAT! - rusty");
            }

            system.setSpeakerMode(FMOD.SPEAKERMODE._7POINT1);
            ERRCHECK(result);

            result = system.init(32, FMOD.INITFLAGS.NORMAL, (IntPtr)null);
            ERRCHECK(result);


            //LOAD IN PATHS OF AUDIO FILES
            string sound1Path = "../../../../FluxContent/audio/drumloop.wav";

            FMOD.Sound tmpSound = null;

            //RUN FOR EACH SOUND FILE
            result = system.createSound(sound1Path, (FMOD.MODE.SOFTWARE | FMOD.MODE._2D), ref tmpSound);
            ERRCHECK(result);
            sounds["drumloop"] = tmpSound;
            result = sounds["drumloop"].setMode(FMOD.MODE.LOOP_OFF);
            ERRCHECK(result);

        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            FMOD.RESULT result;
            uint ms = 0;
            uint lenms = 0;
            bool playing = false;
            bool paused = false;
            int channelsplaying = 0;

            if (channel != null)
            {
                FMOD.Sound currentsound = null;

                result = channel.isPlaying(ref playing);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE) && (result != FMOD.RESULT.ERR_CHANNEL_STOLEN))
                {
                    ERRCHECK(result);
                }

                result = channel.getPaused(ref paused);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE) && (result != FMOD.RESULT.ERR_CHANNEL_STOLEN))
                {
                    ERRCHECK(result);
                }

                result = channel.getPosition(ref ms, FMOD.TIMEUNIT.MS);
                if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE) && (result != FMOD.RESULT.ERR_CHANNEL_STOLEN))
                {
                    ERRCHECK(result);
                }

                channel.getCurrentSound(ref currentsound);
                if (currentsound != null)
                {
                    result = currentsound.getLength(ref lenms, FMOD.TIMEUNIT.MS);
                    if ((result != FMOD.RESULT.OK) && (result != FMOD.RESULT.ERR_INVALID_HANDLE) && (result != FMOD.RESULT.ERR_CHANNEL_STOLEN))
                    {
                        ERRCHECK(result);
                    }
                }

                system.getChannelsPlaying(ref channelsplaying);
            }

            if (system != null)
            {
                system.update();
            }
        }

        public static void Play(string key, int speaker)
        {
            // play audio
            FMOD.RESULT result;

            result = system.playSound(FMOD.CHANNELINDEX.FREE, sounds[key], true, ref channel);
            ERRCHECK(result);

            //SPEAKER 1 (Satelite Audio, Side Specific)
            if (speaker == 0)
            {
                result = channel.setSpeakerMix(0, 0, 0, 0, 0, 0, 1.0f, 0);
                ERRCHECK(result);
            }

            //SPEAKER 2 (Satelite Audio, Side Specific)
            if (speaker == 1)
            {
                result = channel.setSpeakerMix(0, 0, 0, 0, 0, 0, 0, 1.0f);
                ERRCHECK(result);
            }

            //SPEAKER 3 (Satelite Audio, Side Specific)
            if (speaker == 2)
            {
                result = channel.setSpeakerMix(0, 0, 0, 0, 1.0f, 0, 0, 0);
                ERRCHECK(result);
            }

            //SPEAKER (Satelite Audio, Side Specific)
            if (speaker == 3)
            {
                result = channel.setSpeakerMix(0, 0, 0, 0, 0, 1.0f, 0, 0);
                ERRCHECK(result);
            }

            //SPEAKER 5 (Subwoofer)
            if (speaker == 4)
            {
                result = channel.setSpeakerMix(0, 0, 1.0f, 0, 0, 0, 0, 0);
                ERRCHECK(result);
            }

            result = channel.setPaused(false);
            ERRCHECK(result);

        }

        private static void ERRCHECK(FMOD.RESULT result)
        {

            if (result != FMOD.RESULT.OK)
            {
                //timer.Stop();
                //MessageBox.Show("FMOD error! " + result + " - " + FMOD.Error.String(result));
                //Environment.Exit(-1);
            }

        }
    }
}
