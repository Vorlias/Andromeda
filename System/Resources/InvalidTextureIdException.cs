using System;

namespace Andromeda.System
{
    public class InvalidTextureIdException : Exception
    {
        public InvalidTextureIdException(string textureId) : base ("Invalid texture id given: " + textureId)
        {

        }
    }
}
