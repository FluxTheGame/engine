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
            '<p>This is the core of our game controller application. Don\'t<br>forget to open the <a href="https://developers.google.com/chrome-developer-tools/docs/console">JavaScript Console</a> before continuing!</p>',
            '<p class="note"><i>If you\'d like to try out the Node.js implementation, run <br>"<code>node server.js</code>" at /app/resources/scripts/ and<br>refresh the page.</i></p>',
            '<h2>Sencha Touch (2.0.1) &mdash; Node.js (0.8.1)</h2>'
		].join("")
	}
});