/*
 * @file Main.js
 * 
 * Main view that handles tabbed browsing. To add
 * a new tab, add a new object to the items array
 * with the xtype of the object.
 */

Ext.define("GC.view.Main", {
    extend: 'Ext.tab.Panel',
    requires: ['Ext.TitleBar'],

    id: "main",

    config: {

        layout: {
            type: 'card',
            animation: {
                type: 'fade'
            }
        },

        tabBarPosition: 'bottom',

        items: [
            {
                xtype: 'homepanel'
            },
            {
                xtype: 'touchreader'
            }
        ]
    }
});
