# MySnipItTool
MySnipItTool is a screenshot application written in WPF that allows for multiple screenshots to be taken and edited in individual tabs, simultaneously.
This makes dealing with lots of screenshots and multitasking easier.

## Table of Contents

- [Features](#features)
- [Contributing](#contributing)
- [History (for Developers)](#history)
- [License](#license)

## Features

- Simple, easy-to-learn user interface
- Full screenshots or screen snippets
- Annotation of screenshots via basic drawing tools
- Multiple screenshots can be edited simultaneously in separate tabs

![Screenshot of MySnipItTool](Screenshot.jpeg "Screenshot of MySnipItTool")

## Contributing
If you wish to contribute to making MySnipItTool better, you are free to simply clone the project and add the feature you see fit. The code is written in C#, and WPF is the chosen GUI framework, so you just need Visual Studio with the desktop workload features installed. For the less tech-savvy or those not wanting to contribute code-wise, any suggestions/comments/feedback is also welcome.


## History (for Developers)
Originally, the code was written using the Windows Forms (Winforms) GUI framework. The problem with drawing in Winforms is drawing on top of a picture. You can easily draw on top of a picture, but erasing will actually erase the picture itself, there is no type of drawing control or control that would allow easy anchored positioning of GUI items. So, after building a working prototype with Windows Forms, and running into these roadblocks, I scratched the idea of using Winforms and
rebuilt MySnipItTool using WPF (Windows Presentation Foundation). With WPF, I was able to use the magical Canvas control, which allows shapes and other items to be easily added on top. Additionally, with WPF, there was more flexibility in improving the user interface and compartmentalizing it into separate, reusable pieces. For example, the ScreenshotTab is its own UserControl, a Canvas, wrapped around an Image, with its own drawing logic.

## License
MySnipItTool operates under the MIT License.



