﻿using EPiServer.Core;

namespace Optimizely.Demo.ContentTypes.Models.Media.Base;

public abstract class ImageFileBase : ImageData
{
    public virtual string? Copyright { get; set; }
    public virtual string? AlternateText { get; set; }
}
