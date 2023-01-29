# itaa

## Usage
```
itaa.exe
  -i, --image            Required. The image to process

  -o, --output           Required. The output path of the image

  -t, --text             (Default: theskz) The text to write on the image

  --resized-width        (Default: Original width) The width to resize the image before processing

  --font-size            (Default: 10) The text font size

  --character-spacing    (Default: 8) How many pixels to space the characters apart

  --brightness-levels    (Default: 8) How many levels of brightness there are

  --min-letters          (Default: 1) The minimum number of letters per pixel

  --offset               (Default: 2) The drawing offset for letters

  --help                 Display this help screen.

  --version              Display version information.
```

I recommend a resized width of at least 250 or get vertical stripes on your image.
If you have all uppercase text i recommend settings character-spacing to 10, if lowercase 8.
Of course you can experiment with the settings.
