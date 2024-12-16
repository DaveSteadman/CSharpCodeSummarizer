# C# Code Summarizer
- Input: A path to a directory structure full of .cs files that we would aspire to have an LLM conversation use.
- Output: A text file detailing the code:

Example:

```csharp
Class: UIButton
  Attribute: string Text
  Attribute: Action<UIButton> Click
  Method: void Draw(SKCanvas canvas)
  Method: void OnMouseDown(SKPoint point)

Class: UIElement
  Attribute: SKRect Bounds
  Attribute: bool IsVisible
  Attribute: bool IsEnabled
  Method: void Draw(SKCanvas canvas)
  Method: bool HitTest(SKPoint point)
  Method: void OnMouseDown(SKPoint point)
  Method: void OnMouseUp(SKPoint point)
  Method: void OnMouseMove(SKPoint point)

Class: UIImageArea
  Attribute: SKBitmap Image
  Method: void Draw(SKCanvas canvas)

