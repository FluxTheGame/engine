#include "cinder/app/AppBasic.h"
#include "cinder/gl/gl.h"

#include "OscListener.h"
#include "OscSender.h"

using namespace ci;
using namespace ci::app;

class NodeJsApp : public AppBasic {
  public:
	void setup();
	void mouseDown( MouseEvent event );	
	void update();
	void draw();
    
    osc::Listener listener;
    osc::Sender sender;
    
    std::string host;
    int inport, outport;
    
    float posX, posY;
};

void NodeJsApp::setup()
{
    posX = 400;
    
    host    = "localhost";
    inport  = 3333;
    outport = 3334;
    
    listener.setup(inport);
    sender.setup(host, outport);
}

void NodeJsApp::mouseDown( MouseEvent event )
{
}

void NodeJsApp::update()
{
    while (listener.hasWaitingMessages()) {
        osc::Message message;
        listener.getNextMessage(&message);
        
        console() << "New message received" << std::endl;
        console() << "Address: " << message.getAddress() << std::endl;
        console() << "Num Arg: " << message.getNumArgs() << std::endl;
        
        for (int i = 0; i < message.getNumArgs(); i++) {
            console() << "-- Argument " << i << std::endl;
            console() << "---- type: " << message.getArgTypeName(i) << std::endl;
            
            if (message.getArgType(i) == osc::TYPE_INT32) {
                try {
                    console() << "------ value: " << message.getArgAsInt32(i) << std::endl;
                } catch (...) {
                    console() << "Exception reading argument as int32" << std::endl;
                }
            } else if (message.getArgType(i) == osc::TYPE_FLOAT) {
                try {
                    console() << "------ value: " << message.getArgAsFloat(i) << std::endl;
                } catch (...) {
                    console() << "Exception reading argument as float" << std::endl;
                }
                
                posX = message.getArgAsFloat(0);
            } else if (message.getArgType(i) == osc::TYPE_STRING) {
                try {
                    console() << "------ value: " << message.getArgAsString(i) << std::endl;
                } catch (...) {
                    console() << "Exception reading argument as string" << std::endl;
                }
            }
            
            osc::Message message;
            message.addStringArg("Received data!");
            message.setAddress("/draw");
            message.setRemoteEndpoint(host, outport);
            sender.sendMessage(message);
        }
    }
}

void NodeJsApp::draw()
{
    float gray = sin( getElapsedSeconds() ) * 0.5f + 0.5f;
	// clear out the window with black
	gl::clear( Color( gray, gray, gray ), true );
    posY = 100.0f;
    gl::drawSolidCircle( Vec2f( posX, posY ) + getWindowSize() / 2, 50.0f );
}

CINDER_APP_BASIC( NodeJsApp, RendererGl )
