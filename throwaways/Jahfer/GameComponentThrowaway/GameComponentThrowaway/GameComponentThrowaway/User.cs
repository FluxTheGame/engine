using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {

    /// <summary>
    /// 
    /// </summary>
    class User {

        public enum EUserElement {
            EL_FIRE,
            EL_WATER,
            EL_WIND,
            EL_ICE
        };

        // Constructor
        public User() {
            this.ID = getID();
            //Console.WriteLine("User {0} created!", this.ID);
        }

        private static int userID = 0;
        public static int getID() {
            return userID++;
        }


        /// <summary>
        /// 
        /// </summary>
        public int ID;

        /// <summary>
        /// The <c>SelectedElement</c> property represents the currently selected 
        /// element in the user's toolkit.
        /// </summary>
        private EUserElement _selectedElement;

        /// <summary>
        /// The <c>Position</c> element property represents the current on-screen
        /// coordinates of the user.
        /// </summary>
        private Point _position;

        /// <summary>
        /// The <c>releaseSelectedElement</c> method allows the game class to trigger
        /// the drop of the currently selected element, stored in <c>_selectedElement</c>.
        /// </summary>
        /// <returns>Returns the coordinates of the item dropped.</returns>
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
