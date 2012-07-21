using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameComponentThrowaway {

    /// <summary>
    /// 
    /// </summary>
    class Plant {

        /// <summary>
        /// The current "state" of the <c>Plant</c>. The feeding state can be of
        /// the following types: attack, graze, drink; the type depends on the nature
        /// of the animal.
        /// </summary>
        public enum PlantState { BIRTH, DEATH };

        // Constructor
        public Plant(ContentManager content) {
            this.ID = getID();
            this.State = PlantState.BIRTH;
        }

        // Properties
        public int ID { get; private set; }
        public PlantState State { get; private set; }

        // Variables
        private Point _position;


        // Static Methods
        private static int userID = 0;
        public static int getID() { return userID++; }


        // Methods
        public void update() {
            //Console.WriteLine("User {0} has been updated!", this.ID);
        }

        public void draw() {
            //Console.WriteLine("User {0} has been drawn!", this.ID);
        }
    }
}
