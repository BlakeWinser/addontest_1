using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace MinimalExample
{
	public class Crosshair : Panel
	{
        public Panel flexContainer;
        public Panel crosshairItem;
		
		public Crosshair()
		{
			StyleSheet.Load( "ui/Crosshair.scss" );

            flexContainer = Add.Panel("flexContainer");
            crosshairItem = Add.Panel("crosshairItem");
			flexContainer.AddChild(crosshairItem);
			// crosshairItem.AddClass("classname1 classname2"); -- can use Tailwind, as we can use this method to add classes.
		}
    }
}
