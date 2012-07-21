using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GameComponentThrowaway {

    public enum BiomeType {
        FOREST,
        SAVANNA,
        TUNDRA,
        JUNGLE
    };
    
    class Biome {

        public int ID           { get; private set; }
        public BiomeType Type   { get; private set; }
        public int Humidity     { get; private set; }
        public int Temperature  { get; private set; }

        public Biome(int pID, ContentManager content) {
            this.ID = pID;
        }


        public void update() {

        }

        public void draw() {

        }

    }
}
