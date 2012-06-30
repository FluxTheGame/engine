/*
 * @file Game.js
 * 
 * Primary controller for application. Handles loading of views, 
 * tap events of buttons.
 */


Ext.define('GC.controller.Game', {
	extend: 'Ext.app.Controller',

	config: {
    	views: ['Main', 'TouchReader', 'Home', 'Contact', 'Blog'],
		refs: {
			reader: 'touchreader #box'
		},

		// Start listening for events on views
		control: {
			'#firstButton' : {
				tap: function() {
            		Ext.Msg.alert('First button tapped!');
            		console.log('> [controller/Game] First button tapped!');
				}
			},
			'#secondButton' : {
				tap: function() {
            		Ext.Msg.alert('Second button tapped!');
            		console.log('> [controller/Game] Second button tapped!');
				}
			}
		}
	},
})