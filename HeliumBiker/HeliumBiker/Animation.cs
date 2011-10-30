using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeliumBiker
{
    public class Animation
    {
        // La velocidad de animacion
        private float animationRate = 50f;

        #region variables locales
        private Rectangle src;
        private int[] frameCounts;      // Arreglo que muestra las imagenes que tiene cada fila
        private float timer = 0f;       // Tiempo
        private int activeRow;          // Numero que indica en que fila esta
        private int currentFrame;       // Numero que indica en que columna esta
        private bool toggle = false;    // Inversa la imagen por ronda
        private bool reverse = false;   // la sequencia se mueve de derecha a izquierda (al revez) <--- si es verdadero
        private bool stick = false;     // Corre la sequencia y se queda en la ultima imagen
        private bool switcher = false;  // Hace switcher en la imagen
        private float rate = 1f;        // Un escalador para manejar la velocidad de el refrescamiento
        #endregion

        /**
         * Constructor de la clase Animation
         */
        public Animation(int imageSize, int[] frameCounts)
        {
            this.src = new Rectangle(0, 0, imageSize, imageSize);
            this.frameCounts = frameCounts;
            currentFrame = 0;
            activeRow = 0;
        }

        /**
         * Segundo Constructor de la clase Animation
         */
        public Animation(int imageWidth, int imageHeigth, int[] frameCounts)
        {
            this.src = new Rectangle(0, 0, imageWidth, imageHeigth);
            this.frameCounts = frameCounts;
            currentFrame = 0;
            activeRow = 0;
        }

        /**
         * Resetea el formato de recorrido de sequencia
         **/
        public void reset()
        {
            reverse = false;
            stick = false;
        }

        /**
         * Refresca la animacion de la entidad
         */
        public void refresh(float deltaTime)
        {
            timer += deltaTime;
            if (timer > (AnimationRate * Rate))
            {
                if (reverse)
                {
                    if (currentFrame > 0)
                        currentFrame--;
                    else
                    {
                        if (!stick)
                        {
                            currentFrame = frameCounts[activeRow] - 1;
                            toggle = !toggle;
                        }
                    }
                }
                else
                    if (frameCounts[activeRow] - 1 > currentFrame)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        if (!stick)
                        {
                            currentFrame = 0;
                            if (switcher)
                                toggle = !toggle;
                        }
                    }
                timer = 0f;
            }
        }

        # region Gets and Sets
        public float AnimationRate
        {
            get { return animationRate; }
            set { animationRate = value; }
        }
        public Rectangle getSrcRect()
        {
            return new Rectangle(currentFrame * src.Width, activeRow * src.Height, src.Width, src.Height);
        }
        public int getCurrentFrame()
        {
            return currentFrame;
        }
        public void setCurrentFrame(int currentFrame)
        {
            this.currentFrame = currentFrame;
        }
        public int getImageSizeW()
        {
            return src.Width;
        }
        public int getImageSizeH()
        {
            return src.Height;
        }
        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
        public int ActiveRow
        {
            get { return activeRow; }
            set { activeRow = value; }
        }
        public bool Toggle
        {
            get { return toggle; }
            set { toggle = value; }
        }
        public bool Reverse
        {
            get { return reverse; }
            set { reverse = value; }
        }
        public bool Stick
        {
            get { return stick; }
            set { stick = value; }
        }
        public bool Switcher
        {
            get { return switcher; }
            set { switcher = value; }
        }
        public float Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        public int getFrameCount(int index)
        {
            return frameCounts[index];
        }
        # endregion

        internal static Animation getAnimation(Microsoft.Xna.Framework.Graphics.Texture2D t)
        {
            return new Animation(t.Height, new int[] { t.Width/t.Height });
        }

        internal static Animation getAnimation(Microsoft.Xna.Framework.Graphics.Texture2D texture2D, Vector2 size)
        {
            return new Animation((int)size.X, (int)size.Y, new int[] { 1 });
        }
    }
}
