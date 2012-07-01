/*
 * @file app.js
 * 
 * Base configuration file for loading full app. Can load 
 * controllers, views, models. Handle initialization and
 * launch of application.
 */


Ext.application({
    name: 'GC',

    settings: {
        'server' : 'localhost',
        'port' : '8000'
    },

    requires: [
        'Ext.MessageBox'
    ],

    controllers: ['Game', 'Socket'],

    icon: {
        '57': 'resources/icons/Icon.png',
        '72': 'resources/icons/Icon~ipad.png',
        '114': 'resources/icons/Icon@2x.png',
        '144': 'resources/icons/Icon~ipad@2x.png'
    },

    isIconPrecomposed: true,

    startupImage: {
        '320x460': 'resources/startup/320x460.jpg',
        '640x920': 'resources/startup/640x920.png',
        '768x1004': 'resources/startup/768x1004.png',
        '748x1024': 'resources/startup/748x1024.png',
        '1536x2008': 'resources/startup/1536x2008.png',
        '1496x2048': 'resources/startup/1496x2048.png'
    },

    initialize: function() {
        console.log('App initialize');
        this.callParent();
    },

    launch: function() {
        console.log('App launch');
        // Destroy the #appLoadingIndicator element
        Ext.fly('appLoadingIndicator').destroy();

        // Initialize the main view
        Ext.Viewport.add(Ext.create('GC.view.Main'));
    },

    onUpdated: function() {
        Ext.Msg.confirm(
            "Application Update",
            "This application has just successfully been updated to the latest version. Reload now?",
            function(buttonId) {
                if (buttonId === 'yes') {
                    window.location.reload();
                }
            }
        );
    }
});
