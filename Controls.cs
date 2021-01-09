// this file defines the implementation of the keyboard settings
using System;
using SDL2;

// this class defines the keycodes by reading from the settings.txt file
class Controls_setup : Controls{

    // changes the given binding to the given keycode
    private static void change_key_binding(string binding, string activation_keycode){

        SDL.SDL_Keycode key = SDL.SDL_GetKeyFromName(activation_keycode);
        Console.WriteLine($"         control <{binding}> set to keycode <{activation_keycode}> , SDLK key <{key}>");


        if      (binding == "up")       {Controls.up    = key;}
        else if (binding == "down")     {Controls.down  = key;}
        else if (binding == "right")    {Controls.right = key;}
        else if (binding == "left")     {Controls.left  = key;}
        else if (binding == "esc")      {Controls.esc   = key;}
        else if (binding == "enter")    {Controls.enter = key;}
        else if (binding == "tab")      {Controls.tab   = key;}
        else{Console.WriteLine("invalid control mentioned in text tile");}

    }


    // try to read the settings file
    // if successful it will declere the keyboard settings agordingly
    // returns false if successful otherwise returns true
    public static bool register_controls(){

        try {

            // read the settings file
            string text = System.IO.File.ReadAllText("Settings.txt");
            string binding = "";
            string word = "";
            bool read = false;

            // loops through each char in the text
            foreach (char text_char in text){
                // if char = " " then reset the word
                if (text_char == ' '){
                    word = "";
                }
                // if char = ":" then change the binding to the registerd current word
                else if (text_char == ':'){
                    if (read){
                        binding = word;
                        word = "";   
                    }
                }
                // if char = ";" then change the keycode for the latest registerd binding to the current word
                else if (text_char == ';'){
                    if (read){
                        change_key_binding(binding, word);
                        word = "";     
                    }
                }
                // if char = "{" then unlock the ability to change the bindings           
                else if (text_char == '{'){
                    if (word == "Controls"){read = true;}
                    word = "";
                }
                // if char = "}" then lock the ability to change the bindings
                else if (text_char == '}'){
                    read = false;
                    word = "";
                }
                // if char is just a normal letter then add it to the word
                else{
                    word = word + text_char;
                }

            }

            // if successful return true
            return false;

        }
        catch (Exception){

            // if unsuccesful return false
            Console.WriteLine("Error, could not register keyboard controls\n consider fixing the Settings.txt file");
            return true;

        }

    }

}



// need separate class for the keyboard settings used during gameplay
class Controls{

    // the program uses this variable when proccesing the user input events
    private static SDL.SDL_Event event_variable;


    // controls settings (with their index values in the key_pressed_bit_map)

    protected static SDL.SDL_Keycode right_mouse_button;   // 0
    protected static SDL.SDL_Keycode left_mouse_button;    // 1
    protected static SDL.SDL_Keycode middle_mouse_button;  // 2

    protected static SDL.SDL_Keycode up;                   // 3
    protected static SDL.SDL_Keycode down;                 // 4
    protected static SDL.SDL_Keycode right;                // 5
    protected static SDL.SDL_Keycode left;                 // 6
    protected static SDL.SDL_Keycode esc;                  // 7
    protected static SDL.SDL_Keycode enter;                // 8
    protected static SDL.SDL_Keycode tab;                  // 9


    // cursor cordinets
    public static int cursor_click_x;
    public static int cursor_click_y;
    public static int cursor_lifted_x;
    public static int cursor_lifted_y;
    public static int cursor_current_x;
    public static int cursor_current_y;


    // this array contains a bool for each key
    // if the key is being pressed then the bool is set to true
    // else the bool is set to false
    public static bool[] key_pressed_bit_map = new bool[10] {
        false, false, false, false, false, false, false, false, false, false
    };
    public static bool quit = false;

    // registers key presses
    private static void key_pressed(SDL.SDL_Keycode key){

        if      (key == up)     {key_pressed_bit_map[3] = true;}
        else if (key == down)   {key_pressed_bit_map[4] = true;}
        else if (key == right)  {key_pressed_bit_map[5] = true;}
        else if (key == left)   {key_pressed_bit_map[6] = true;}
        else if (key == esc)    {key_pressed_bit_map[7] = true;}
        else if (key == enter)  {key_pressed_bit_map[8] = true;}
        else if (key == tab)    {key_pressed_bit_map[9] = true;}

    }

    // remove keypresses when key is released
    private static void key_lifted(SDL.SDL_Keycode key){

        if      (key == up)     {key_pressed_bit_map[3] = false;}
        else if (key == down)   {key_pressed_bit_map[4] = false;}
        else if (key == right)  {key_pressed_bit_map[5] = false;}
        else if (key == left)   {key_pressed_bit_map[6] = false;}
        else if (key == esc)    {key_pressed_bit_map[7] = false;}
        else if (key == enter)  {key_pressed_bit_map[8] = false;}
        else if (key == tab)    {key_pressed_bit_map[9] = false;}

    }

    // registers mouse clicks
    private static void mouse_pressed(SDL.SDL_MouseButtonEvent click){

        if      (click.button == 1){key_pressed_bit_map[0] = true;}     // right mouse button
        else if (click.button == 3){key_pressed_bit_map[1] = true;}     // left mouse button
        else if (click.button == 2){key_pressed_bit_map[2] = true;}     // middle mouse button
        cursor_click_x = click.x;
        cursor_click_y = click.y;

    }

    // registers mouse click releases
    private static void mouse_lifted(SDL.SDL_MouseButtonEvent click){

        if      (click.button == 1){key_pressed_bit_map[0] = false;}     // right mouse button
        else if (click.button == 3){key_pressed_bit_map[1] = false;}     // left mouse button
        else if (click.button == 2){key_pressed_bit_map[2] = false;}     // middle mouse button
        cursor_lifted_x = click.x;
        cursor_lifted_y = click.y;

    }

    private static void mouse_moved(SDL.SDL_MouseMotionEvent motion){

        cursor_current_x = motion.x;
        cursor_current_y = motion.y;
        //Console.WriteLine($"x : {motion.x}\ny : {motion.y}");

    }

    // updates user inputs from mouse and keyboard
    // sets quit variable to true if the user input would be to quit
    public static void update_user_input(){

        // SDL2 event loop
        // registers user input
        while ( SDL.SDL_PollEvent(out event_variable) != 0 ){

            switch (event_variable.type){
                
                // quiting via x buttom
                case SDL.SDL_EventType.SDL_QUIT:
                    quit = true;
                    break;
                // if a key is pressed
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    Controls.key_pressed(event_variable.key.keysym.sym);
                    break;
                // if a key is released
                case SDL.SDL_EventType.SDL_KEYUP:
                    Controls.key_lifted(event_variable.key.keysym.sym);
                    break;
                // if mouse button is pressed
                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    Controls.mouse_pressed(event_variable.button);
                    break;
                // if mouse button is released
                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    Controls.mouse_lifted(event_variable.button);
                    break;
                // if mouse moves
                case SDL.SDL_EventType.SDL_MOUSEMOTION:
                    Controls.mouse_moved(event_variable.motion);
                    break;

            }

        }

    }

}
