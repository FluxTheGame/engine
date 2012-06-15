Ext.define("TouchEvents_001.view.Main", {
    extend: 'Ext.tab.Panel',
    requires: [
        'Ext.TitleBar',
        'Ext.Video'
    ],
    config: {
        tabBarPosition: 'bottom',

        items: [
            {
                title: 'Welcome',
                iconCls: 'home',

                styleHtmlContent: true,
                scrollable: true,

                items: {
                    docked: 'top',
                    xtype: 'titlebar',
                    title: 'Welcome to Sencha Touch 2'
                },
                {
					id: 'tapInfo',
					styleHtmlContent: true,
					html: 'Data will appear here',
					padding: 10
				}
            },
            {
                title: 'Get Started',
                iconCls: 'action',

                items: [
                    {
                        docked: 'top',
                        xtype: 'titlebar',
                        title: 'Getting Started'
                    },
                    {
                        xtype: 'video',
                        url: 'http://av.vimeo.com/64284/137/87347327.mp4?token=1330978144_f9b698fea38cd408d52a2393240c896c',
                        posterUrl: 'http://b.vimeocdn.com/ts/261/062/261062119_640.jpg'
                    }
                ]
            }
        ]
    }
});

Ext.Viewport.element.on({
    tap: function(e, node) {
        var string = '';

        string += 'You tapped at: <strong>{ x: ' + e.pageX + ', y: ' + e.pageY + ' }</strong> <i>(e.pageX & e.pageY)</i>';
        string += '<hr />';
        string += 'The HTMLElement you tapped has the className of: <strong>' + e.target.className + '</strong> <i>(e.target)</i>';
        string += '<hr />';
        string += 'The HTMLElement which has the listener has a className of: <strong>' + e.getTarget().className + '</strong> <i>(e.getTarget())</i>';

        Ext.getCmp('logger').setHtml(string);
    }
});
