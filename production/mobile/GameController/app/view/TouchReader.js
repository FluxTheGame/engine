/*
 * @file TouchReader.js
 * 
 * Bloated view to create the UI of the "Touch" tab. Also
 * contains the event listener for drag events on the touchbox.
 * Not really sure how to move that to controllers/Game.
 */


Ext.define('GC.view.TouchReader', {

	extend: 'Ext.Container',
	xtype: 'touchreader',
    fullscreen: true,
    layout: 'hbox',

	config: {
		title: 'Touch',
		iconCls: 'arrow_right',
		id: 'touchbox',
		layout: 'hbox',

		scrollable: false,
		styleHtmlContent: true,

		// visual components of the page
		items : [			
			{ 
				// create button container
				xtype: 'container',
    			layout: 'vbox',
    			flex: 1,
				items : [
					// create buttons
					{
						xtype: 'button',
						id: 'firstButton',
						text: 'Click me!', 
						flex: 1						
					},
					{
						xtype: 'button',
						id: 'secondButton',
						text: 'Click me too!', 
						flex: 1						
					},
				]

			},

			// create touchbox
			{
				xtype: 'container',
				html: "Hey!",
				id: "box",
				flex: 2,

				// dom element has loaded, lets hook up an event listener!
				initialize: function() {
					this.getEventDispatcher().addListener('element', '#box', '*', this.onTouchPadEvent, this);
				},

				// handle event on element
				onTouchPadEvent: function(e, target, options, eventController) {
					var eventName = eventController.info.eventName;

					if (eventName.match("drag")) {
						// e contains all the info for positioning
						console.log("> [view/TouchReader]", eventName, e.deltaX, e.deltaY);
					}
				}
			}
		]
	}
});