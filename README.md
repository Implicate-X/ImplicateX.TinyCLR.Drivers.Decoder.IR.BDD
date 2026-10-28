# 📡 Binary Distance Decoder
## 🎯 Overview
The Binary Distance Decoder (BDD) is a lightweight infrared receiver and decoder for Binary Distance encoded protocols.<br/>
The decoder transforms a time-continuous pulse stream into discrete command events.
Instead of continuously polling an IR receiver or manually processing pulse timings, the decoder converts incoming IR frames into high level events and exposes the decoded command as a simple numeric value.<br/>
The application only reacts to decoded commands.

## ✨ Features
- Real-time IR frame decoding
- Automatic RC5 / Binary Distance frame interpretation
- Event-driven architecture
- Toggle bit handling
- Address and command extraction
- Low resource consumption
- Designed for TinyCLR and embedded systems
- No protocol-specific logic required in the application layer

## 📖 Binary Distance Encoding

Binary Distance protocols encode information using the distance between carrier bursts.

Bit Representation
| Bit | Pulse  | Space   |
|:----|:------:|--------:|
| 0   | 600 µs | 600 µs |
| 1   | 600 µs | 1600 µs |

A synchronization gap larger than approximately 8 ms marks the beginning of a new frame.

## 🧩 Frame Structure

| Toggle | Address | Command |
|:------:|:-------:|:-------:|
| 1 Bit  | 5 Bits  | 7 Bits  |

| Field   | Description                                |
|:-------|:-------------------------------------------|
| Toggle | Alternates between consecutive button presses |
| Address | Device identifier                           |
| Command | Function code                               |

## 🚀 Getting Started
Instantiate the Decoder
### C#
```csharp
// Binary Distance Decoder
 
var bdd = new BinaryDistanceDecoder( remoteCtrlPin );
```
### 📨 FrameDecoded Event
Subscribe to Decoded Frames
### C#
```csharp
bdd.FrameDecoded += ( arg ) =>
{
    Debug.WriteLine(
        $"Frame: 0x{arg.RawValue:X4}"
    );
};
```
Whenever a valid frame is received, the decoder raises the FrameDecoded event.

### 🎮 DENON Remote Control Example
### C#
```csharp
bdd.FrameDecoded += ( arg ) =>
{
    Code.DENON.TryGetValue(
        arg.RawValue,
        out string commandName );

    if( commandName == "Tuning +" )
    {
        ApplyTune(
            true,
            "Tune Up command received." );
    }
    else if( commandName == "Tuning -" )
    {
        ApplyTune(
            false,
            "Tune Down command received." );
    }
};
```

## ⚙️ Design Philosophy

The Binary Distance Decoder follows the same philosophy used throughout the WebTuner project:<br/>
Hardware-specific signal processing belongs in the driver layer.<br/>
Applications should consume meaningful events, not electrical waveforms.<br/>
The decoder transforms a time-continuous pulse stream into discrete command events.<p/>
Part of the WebTuner project and designed for TinyCLR-based embedded systems. 🚀

## 🔬 Typical Applications
- FM tuners
- DAB receivers
- Audio amplifiers
- Embedded user interfaces
- Home automation systems
- Laboratory equipment

## 📚 Protocol Background

The BDD implementation is particularly suited for RC5-style Binary Distance encoded infrared protocols where bit values are determined by pulse spacing rather than pulse width alone.

## 📄 License

Independent part of the WebTuner project.

## ❤️ TinyCLR OS

Designed for modern embedded development using .NET and TinyCLR OS.
