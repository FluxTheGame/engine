/*
 * @file TouchReader.js
 * 
 * Render the "Home" tab
 */



Ext.define('GC.view.Home', {
	extend: 'Ext.Panel',
	xtype: 'homepanel',

	config: {
		title: 'Home',
		iconCls: 'home',
		cls: 'home',

		scrollable: true,
		styleHtmlContent: true,

		html: [
			'<img id="logo" src="http://jahfer.com/dev/img/projectorface.png" />',
            '<h1>Welcome ProjectorFace Members!</h1>',
            '<p>This is the core of our game controller application.<br>Don\'t forget to open the <a href="https://developers.google.com/chrome-developer-tools/docs/console">JavaScript Console</a> before continuing!</p>',
            '<h2>Sencha Touch (2.0.1)</h2>'
		].join("")
	}
});