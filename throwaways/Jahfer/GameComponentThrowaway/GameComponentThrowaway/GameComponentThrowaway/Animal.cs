using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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
        public Animal(ContentManager content) {
            this.ID = getID();
            this.Type = AnimalType.OMNIVORE;
            this.State = AnimalState.BIRTH;

            theModel = content.Load<Model>("Models\\p1_wedge");
        }

        private static int userID = 0;
        public static int getID() {
            return userID++;
        }

        public int ID { get; private set; }
        
        // 3D model
        private Model theModel;

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
        private Vector3 _position   = Vector3.Zero;
        private Vector3 _velocity   = Vector3.Zero;
        private float modelRotation = 0.0f;

        public void Update() {
            //Console.WriteLine("User {0} has been updated!", this.ID);
        }

        public void Draw() {
            //Console.WriteLine("User {0} has been drawn!", this.ID);
        }
    }
}
