# SDL2-CS-SDK
My personal attempt to create a set of C# files that can be easily reused for all my future SDL2 projects.


### How should i use this?
These files does nothing on their own.  
Your meant to copy the files or code that would be useful for you.  
For example if the only thing you wanted was to be able to open a window then you can just copy the Visual.cs and Visual_data.cs files and make use of the "intilase_SDL2_visuals" function and ignore everything else.  

### Are there any dependices?
Your runtime envoriment needs acces to the SDL2.dll file which can be found at https://www.libsdl.org/download-2.0.php  
You also need a wrapper which can be found at https://www.nuget.org/packages/sdl2.nuget/  
The Controls.cs file won't work without Settings.txt file but is otherwise completely independent  
The Visual.cs file won't work without the Visual_data.cs file but is otherwise completly independent
