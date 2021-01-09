using System;
using SDL2;

class Visual : Visual_data {

    // all SDL2 realated graphic assets will be defined here
    private static IntPtr window;
    private static IntPtr renderer;

    // intilases SDL2 realated graphic
    public static void intilase_SDL2_visuals() {

        // initilize SDL2 realated functions
        Console.WriteLine("         Init SDL video");
        SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
        Console.WriteLine("         Init SDL image");
        SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

        // initilize the window
        Console.WriteLine("         Creating the window");
        window = SDL.SDL_CreateWindow("Unreachable", 100, 50, Visual_data.window_width, Visual_data.window_height, SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        // creates a redner objects which is attached to a given window
        Console.WriteLine("         Creating the renderer");
        renderer = SDL.SDL_CreateRenderer(window, 0, 0);

    }

    // sets the color of the renderer
    public static void change_renderer_color(int red, int green, int blue){

        SDL.SDL_SetRenderDrawColor(renderer, Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue), 0);

    }

    // clears the entire window with black color
    public static void clear_screen(){

        SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
        SDL.SDL_RenderClear(renderer);

    }

    // displays the new frame
    public static void show_frame(){

        SDL.SDL_RenderPresent(Visual.renderer);

    }

    // should only be called for debugging purposes
    // not very efficient especialy for big cirlces
    // can not draw bigger circles then r = 255
    public static void draw_small_circle(int x_cord, int y_cord, int radius){


        SDL.SDL_Point[] pixels = new SDL.SDL_Point[Visual_data.circle_pixels[radius]];
        int counter_0 = -radius;
        int counter_1;
        int counter_2 = 0;
        while (counter_0 < radius * 2){

            //
            counter_1 = -radius;

            while (counter_1 < radius * 2){

                // if x * x + y * y = r * r
                int x = counter_0;
                int y = counter_1;
                int area = radius * radius;

                if (area > x * x + y * y){

                    pixels[counter_2].x = x_cord + x;
                    pixels[counter_2].y = y_cord + y;
                    counter_2++;

                }

                counter_1++;

            }

            counter_0++;

        }

        SDL.SDL_RenderDrawPoints(renderer, pixels, counter_2);

    }

    // draws and fills a given rectangle
    public static void draw_sqaure(ref SDL.SDL_Rect rect){

        SDL.SDL_RenderDrawRect(renderer, ref rect);

    }

    // draws but does not fill a given rectangle
    public static void draw_sqaure_border(ref SDL.SDL_Rect rect){

        SDL.SDL_RenderDrawRect(renderer, ref rect);

    }


    // copies the texture_area in the texture into the screen area
    public static void draw_texture(IntPtr texture, ref SDL.SDL_Rect texture_area, ref SDL.SDL_Rect screen_area){

        // texture is the texture set
        // texture_area is the area of the texture set we wan't to draw
        // screen_area is the area of the window we want to draw the texture
        SDL.SDL_RenderCopy(renderer, texture, ref texture_area, ref screen_area);

    }

}
