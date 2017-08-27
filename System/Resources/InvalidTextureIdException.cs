using System;

namespace Vorlias2D.System
{
    public class InvalidTextureIdException : Exception
    {
        public InvalidTextureIdException(string textureId) : base ("Invalid texture id given: " + textureId)
        {

        }
    }
}
