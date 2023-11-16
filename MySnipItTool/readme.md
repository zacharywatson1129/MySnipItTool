This project started with the goal of being an alternative to the Windows Snipping Tool. While the Windows Snipping Tool is very quick, easy to use, and offers most 
users everthing they could ever dream of needing in a screen clipping application, it is not the best tool for power users. Businesses, schools, gamers, tutorial
makers--those kind of people often times wish they could take multiple shots at one time, edit them, and easily change them. That is the exact goal of this project.
MySnipItTool was started with the idea of providing everyday users a free, easy-to-use tool that would make their day more productive and enjoyable. This project
is not intended to create an application that replaces a paint app. While this tool should provide users with basic image editing tools--tools that a person taking
a screenshot might need, like a circle tool to highlight something and a rectangle tool to show users which button to press, this is not an image editing application
and thus its development should focus on improving the screenshot side of things. For instance, an improvement might be saving pictures to a database, or creating
a feature that allows you to send images to a photo-editing app. In summary, the tool is designed for making taking screenshots simpler yet being able to have editing
tools and a tabular interface possible, not for creating and editing images.

As stated in the first of this ReadMe file, the goal of this project is to act as a screenshot tool, which is achieved through a tabular interface, and quick editing
tools all packaged in a simple user interface. This is a useful application because it lets you take multiple screenshots at one time without having to save them, due
to the application being tabular, and it's easy to use because the interface isn't cluttered. It's also a useful tool because their are more editing tools than what
you get with the standard Snipping Tool, making it a bit more customizable for annotating screenshots.
 
Getting started with this project is pretty simple. Simply download the source code as a zip file off of the MySnipItTool Github page, unzip it, and then open the
contents in Visual Studio. As far as standards go, their is no standard, but I, as the author of MySnipItTool, ask out of respect for all developers and the community
that you document your code. If you make any changes or add new features to the source code then I ask, again, out or respect for all developers and the community,
that you document that feature to the best of your ability, at least by making a few simple notes. I also ask that anybody who wishes to contribute please keep
their code neat and clean, and as simple as possible. Their are no standards for code comments in C#, the language in which MySnipItTool is written, so as stated,
their is no one way to document it. Here is an example directly from the code below:

    private void OpenSettingsDialog(object sender, RoutedEventArgs e)
    {
       // Creates a settings dialog and shows it.
       SettingsDialog dialog = new SettingsDialog();
       dialog.ShowDialog();
    }

The one simple comment, "Creates a settings dialog and shows it.", is enough for any user to know that this bit of code does two things, creates a settings dialog,
and then it shows it. This is something a developer of any experience level could understand. The code could have been more complex, such as this:

    /// <summary>
    /// Creates a settings dialog and shows it.
    /// </summary>
    /// <param name="sender">The UI element (menu button) which invoked the event.</param>
    /// <param name="e">The object which contains details on the event.</param>
    private void OpenSettingsDialog(object sender, RoutedEventArgs e)
    {
        SettingsDialog dialog = new SettingsDialog();
        dialog.ShowDialog();
    }

This is perfectly fine, and in fact, lots of developers would love to see your code documented this way. The purpose of the tool, in use, is to make things simple
for the user, with them still getting more. I believe the code should be treated this way as well. I also believe, however, that you can't have too much
documentation, as the more documentation you have in your code, the more the user will be able to understand it. I know that finding the perfect balance in how
many code comments to include while not clogging up source code can be a challenge, but I believe it takes a community effort to build upon the code to make it high
quality, so I believe that the way to be a better contributor is to treat the source code at all times as if a newbie will be reading it--someone who doesn't know
much about the project, probably not a lot about code, and someone who will probably get scared if they find the source code file is thousands of lines of code long.
And if a newbie is scared, they may not try and dive into the project, which is good for neither the project nor the newbie. At the same time, an experienced user,
depending on their views of code maintence and commenting standards, may view too many comments as a good or bad thing. They may think too many comments are a bad
thing, but not descriptive enough code comments (or not enough of them) can lead to their disapproval and they may look down upon the project. All in all, in my view,
it boils down to keeping a balance between keeping the comments simple (and as few as possible) and still keeping the code documented enough to be understandable for
everyone.

As a last and final section to cover in this readme file, I shall cover getting help and support. As a community, anybody who has questions can feel free to post a
question on Github, and in the future, hopefully there will be a special email created for MySnipItTool.
