﻿namespace Optimizely.Demo.ContentTypes.Models.Blocks.Interfaces;

public interface IPreviewCustomHtml
{
    string FirstLevelTag { get; }
    string FirstLevelCssClass { get; }
    string SecondLevelTag { get; }
    string SecondLevelCssClass { get; }
}
