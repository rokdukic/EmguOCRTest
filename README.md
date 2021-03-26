# EmguOCRTest

Description
Simple application meant for testing out OCR with using Emgu.

There are two functions
1. test - this loads an image and applies thresholding based on the input (that can be between 0 - 255). The program cycles until you type in "return". Since I didn't know how to create a window with a preview, every time the image is filtered, the result is saved as "threshold.png".

2. DoOcr - OCR is executed with the filtered image (that's result from test). The extracted test is written in console. Also there's an image that has drawn rectangles of text detections saved as "result.png".
