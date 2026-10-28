using System;
using GHIElectronics.TinyCLR.Pins;
using GHIElectronics.TinyCLR.Devices.Gpio;
using System.Diagnostics;

namespace ImplicateX.TinyCLR.Drivers.Decoder.IR
{
	internal class Program
	{
		public static class ControllerPin
		{
			public const int RemoteCtrl = FEZFeather.GpioPin.PA0;
		}

		private static GpioController gpioController;
		private static BinaryDistanceDecoder binaryDistanceDecoder;

		static void Main()
		{
			gpioController = GpioController.GetDefault();

			GpioPin remoteCtrlPin = gpioController.OpenPin( ControllerPin.RemoteCtrl );
			remoteCtrlPin.SetDriveMode( GpioPinDriveMode.Input );

			binaryDistanceDecoder = new( remoteCtrlPin );

			binaryDistanceDecoder.FrameDecoded += ( arg ) =>
			{
				BinaryDistanceDecoder.Code.DENON.TryGetValue( arg.RawValue, out string commandName );

				if( commandName == "Tuning +" )
				{
					Debug.WriteLine( "Tune Up command received." );
				}
				else if( commandName == "Tuning -" )
				{
					Debug.WriteLine( "Tune Down command received." );
				}
			};
		}
	}
}
