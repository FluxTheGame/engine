using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {

    public enum AnimalType {
        HERBIVORE,
        CARNIVORE,
        OMNIVORE
    };

    /// <summary>
    /// 
    /// </summary>
    class Animal {

        /// <summary>
        /// The current "state" of the <c>Animal</c>. The feeding state can be of
        /// the following types: attack, graze, drink; the type depends on the nature
        /// of the animal.
        /// </summary>
        public enum AnimalState {
            BIRTH,
            DEATH,
            FEED // -> attack, graze or drink depending on type
        };

        // Constructor
        public Animal() {
            this.ID = getID();
            this.Type = AnimalType.OMNIVORE;
            this.State = AnimalState.BIRTH;
        }

        private static int userID = 0;
        public static int getID() {
            return userID++;
        }

        public int ID { get; private set; }

        /// <summary>
        /// The <c>State</c> property represents the current state of the 
        /// animal on-screen.
        /// </summary>
        public AnimalState State { get; private set; }

        /// <summary>
        /// The designated type of the <c>Animal</c>. This property controls the behaviours
        /// of the <c>Animal</c> when interacting with the biome and other <c>Animal</c>s.
        /// </summary>
        public AnimalType Type { get; private set; }

        /// <summary>
        /// The <c>Position</c> element property represents the current on-screen
        /// coordinates of the animal.
        /// </summary>
        private Point _position;



        public void update() {
            //Console.WriteLine("User {0} has been updated!", this.ID);
        }

        public void draw() {
            //Console.WriteLine("User {0} has been drawn!", this.ID);
        }
    }
}
