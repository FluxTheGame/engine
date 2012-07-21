using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameComponentThrowaway {

    public enum Element {
        FIRE,
        WATER,
        WIND,
        ICE
    };

    /// <summary>
    /// 
    /// </summary>
    class User {

        // Constructor
        public User(ContentManager content) {
            this.ID = getID();
            this._selectedElement = Element.FIRE;
        }


        // Properties
        public int ID { get; private set; }


        // Variables
        private Element _selectedElement;
        private Point _position;


        // Static Methods
        private static int userID = 0;
        public static int getID() { return userID++; }


        // Methods
        public void releaseSelectedElement() {
            Point x = this._position;
        }

        public void update() {
            //Console.WriteLine("User {0} has been updated!", this.ID);
        }

        public void draw() {
            //Console.WriteLine("User {0} has been drawn!", this.ID);
        }
    }
}
