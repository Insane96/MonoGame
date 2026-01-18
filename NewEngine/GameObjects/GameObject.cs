using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;

namespace NewEngine.GameObjects;

/// <summary>
/// Abstract base class for all game objects. Provides common functionality for
/// positioning, rendering, collision detection, and lifecycle management.
/// </summary>
/// <param name="sprite">Optional initial sprite for the GameObject.</param>
public abstract class GameObject(AnimatedSprite? sprite = null)
{
    /// <summary>
    /// Gets or sets the position of the GameObject in world coordinates.
    /// </summary>
    public Vector2 Position { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate of the position.
    /// </summary>
    public float X
    {
        get => this.Position.X;
        set => this.Position = new Vector2(value, this.Position.Y);
    }

    /// <summary>
    /// Gets or sets the Y coordinate of the position.
    /// </summary>
    public float Y
    {
        get => this.Position.Y;
        set => this.Position = new Vector2(this.Position.X, value);
    }

    /// <summary>
    /// Gets the scaled width of the GameObject's sprite. Returns 0 if no sprite is set.
    /// </summary>
    public int Width
    {
        get
        {
            if (this.Sprite == null)
                return 0;
            return (int)(this.Sprite.Width * this.Scale.X);
        }
    }

    /// <summary>
    /// Gets the scaled height of the GameObject's sprite. Returns 0 if no sprite is set.
    /// </summary>
    public int Height
    {
        get
        {
            if (this.Sprite == null)
                return 0;
            return (int)(this.Sprite.Height * this.Scale.Y);
        }
    }

    /// <summary>
    /// Gets or sets the rotation angle in radians. Returns 0 if no sprite is set.
    /// </summary>
    public float Rotation
    {
        get => this.Sprite?.Rotation ?? 0f;
        set {
            if (this.Sprite != null)
                this.Sprite.Rotation = value;
        }
    }

    /// <summary>
    /// Gets or sets the scale factor. Returns <see cref="Vector2.One"/> if no sprite is set.
    /// </summary>
    public Vector2 Scale
    {
        get => this.Sprite?.Scale ?? Vector2.One;
        set {
            if (this.Sprite != null)
                this.Sprite.Scale = value;
        }
    }

    /// <summary>
    /// Gets or sets the tint color. Returns <see cref="Color.White"/> if no sprite is set.
    /// </summary>
    public Color Color
    {
        get => this.Sprite?.Color ?? Color.White;
        set {
            if (this.Sprite != null)
                this.Sprite.Color = value;
        }
    }

    /// <summary>
    /// Gets or sets the sprite effects (e.g., flip horizontally/vertically).
    /// Returns <see cref="Microsoft.Xna.Framework.Graphics.SpriteEffects.None"/> if no sprite is set.
    /// </summary>
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
    /// Gets or sets the origin point as a percentage (0~1) of the sprite dimensions.
    /// (0,0) is top-left, (0.5,0.5) is center, (1,1) is bottom-right.
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

    /// <summary>
    /// Gets the axis-aligned bounding box of the GameObject, accounting for origin and scale.
    /// </summary>
    public Rectangle Bounds => new(
        (int)(Position.X - Width * Origin.X),
        (int)(Position.Y - Height * Origin.Y),
        Width,
        Height
    );

    /// <summary>
    /// Gets or sets whether this GameObject is updated each frame.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets or sets whether this GameObject is drawn each frame.
    /// </summary>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// If true, the GameObject will be removed at the end of the current update frame.
    /// </summary>
    public bool RemovalMark { get; set; }

    /// <summary>
    /// Gets or sets the rendering layer. Lower values are drawn first (behind higher values).
    /// </summary>
    public int Layer { get; set; } = 0;

    /// <summary>
    /// Gets the animated sprite associated with this GameObject, or null if none.
    /// </summary>
    public AnimatedSprite? Sprite { get; private set; } = sprite;

    /// <summary>
    /// Initializes the GameObject. Called when instantiated via <see cref="GameObjectManager.Instantiate"/>.
    /// </summary>
    /// <param name="contentManager">The content manager for loading assets.</param>
    public virtual void Initialize(ContentManager contentManager)
    {
        LoadContent(contentManager);
    }

    /// <summary>
    /// Loads content for the GameObject. Sets <see cref="Visible"/> to false if no sprite is set.
    /// </summary>
    /// <param name="contentManager">The content manager for loading assets.</param>
    public virtual void LoadContent(ContentManager contentManager)
    {
        if (this.Sprite == null)
            this.Visible = false;
    }

    /// <summary>
    /// Updates the GameObject each frame. Override to add custom update logic.
    /// </summary>
    public virtual void Update()
    {
        this.Sprite?.Update((float)Time.DeltaTime);
    }

    /// <summary>
    /// Draws the GameObject. Override to add custom rendering logic.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used for drawing.</param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        this.Sprite?.Draw(spriteBatch, this.Position);
    }

    /// <summary>
    /// Enables the GameObject, allowing it to be updated.
    /// </summary>
    public virtual void Enable() => Enabled = true;

    /// <summary>
    /// Disables the GameObject, preventing it from being updated.
    /// </summary>
    public virtual void Disable() => Enabled = false;

    /// <summary>
    /// Shows the GameObject, allowing it to be drawn.
    /// </summary>
    public virtual void Show() => Visible = true;

    /// <summary>
    /// Hides the GameObject, preventing it from being drawn.
    /// </summary>
    public virtual void Hide() => Visible = false;

    /// <summary>
    /// Marks the GameObject for removal at the end of the current update frame.
    /// </summary>
    public virtual void MarkForRemoval() => RemovalMark = true;

    /// <summary>
    /// Disables and hides the GameObject.
    /// </summary>
    public virtual void HideAndDisable()
    {
        Disable();
        Hide();
    }

    /// <summary>
    /// Sets the sprite and centers the origin.
    /// </summary>
    /// <param name="sprite">The new sprite to use.</param>
    public void SetSprite(AnimatedSprite sprite)
    {
        this.Sprite = sprite;
        this.Origin = Origins.Center;
    }

    /// <summary>
    /// Moves the GameObject by the specified offset.
    /// </summary>
    /// <param name="value">The offset to add to the current position.</param>
    public void Move(Vector2 value) => this.Position += value;
}