﻿using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using Optimizely.Demo.ContentTypes.Models.Media.Base;

namespace Optimizely.Demo.ContentTypes.Models.Media;

[ContentType(GUID = "{7E8390E0-8415-4BF8-B91B-1B2517406EA9}")]
[MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png")]
public class ImageFile : ImageFileBase
{
    public virtual string? Copyright { get; set; }
    public virtual string? AlternateText { get; set; }
}
