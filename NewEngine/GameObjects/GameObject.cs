using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;

namespace NewEngine.GameObjects;

public abstract class GameObject(AnimatedSprite? sprite = null)
{
    public Vector2 Position { get; set; }

    public float X
    {
        get => this.Position.X;
        set => this.Position = new Vector2(value, this.Position.Y);
    }

    public float Y
    {
        get => this.Position.Y;
        set => this.Position = new Vector2(this.Position.X, value);
    }
    
    public int Width
    {
        get
        {
            if (this.Sprite == null)
                return 0;
            return (int)(this.Sprite.Width * this.Scale.X);
        }
    }

    public int Height
    {
        get
        {
            if (this.Sprite == null)
                return 0;
            return (int)(this.Sprite.Height * this.Scale.Y);
        }
    }
    
    public float Rotation
    {
        get => this.Sprite?.Rotation ?? 0f;
        set {
            if (this.Sprite != null)
                this.Sprite.Rotation = value;
        }
    }

    public Vector2 Scale
    {
        get => this.Sprite?.Scale ?? Vector2.One;
        set {
            if (this.Sprite != null)
                this.Sprite.Scale = value;
        }
    }

    public Color Color
    {
        get => this.Sprite?.Color ?? Color.White;
        set {
            if (this.Sprite != null)
                this.Sprite.Color = value;
        }
    }

    public SpriteEffects SpriteEffects
    {
        get => this.Sprite?.SpriteEffects ?? SpriteEffects.None;
        set {
            if (this.Sprite != null)
                this.Sprite.SpriteEffects = value;
        }
    }
    
    private Vector2 _origin;
    /// <summary>
    /// Gets or Sets the x- and y-coordinate (in percentage 0~1) of origin to apply when rendering the <see cref="Sprite" />.
    /// </summary>
    public Vector2 Origin
    {
        get => this._origin;
        set
        {
            this._origin = value;
            if (this.Sprite != null)
                this.Sprite.Origin = new Vector2(this.Sprite.Width * _origin.X, this.Sprite.Height * _origin.Y);
        }
    }

    public bool Enabled { get; set; } = true;
    public bool Visible { get; set; } = true;
    /// <summary>
    /// If true, the GameObject is marked for removal and will be removed at the end of the current update frame
    /// </summary>
    public bool RemovalMark { get; set; }

    public int Layer { get; set; } = 0;

    public AnimatedSprite? Sprite { get; private set; } = sprite;

    public virtual void Initialize(ContentManager contentManager)
    {
        LoadContent(contentManager);
    }

    public virtual void LoadContent(ContentManager contentManager)
    {
        if (this.Sprite == null)
            this.Visible = false;
    }

    public virtual void Update()
    {
        this.Sprite?.Update((float)Time.DeltaTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        this.Sprite?.Draw(spriteBatch, this.Position);
    }

    public virtual void Enable() => Enabled = true;
    public virtual void Disable() => Enabled = false;

    public virtual void Show() => Visible = true;
    public virtual void Hide() => Visible = false;
    public virtual void MarkForRemoval() => RemovalMark = true;

    public virtual void HideAndDisable()
    {
        Disable();
        Hide();
    }
    
    public void SetSprite(AnimatedSprite sprite)
    {
        this.Sprite = sprite;
        this.Origin = Origins.Center;
    }

    public void Move(Vector2 value) => this.Position += value;
}