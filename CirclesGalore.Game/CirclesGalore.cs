using osu.Framework;
using osu.Framework.Graphics;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Allocation;
using osu.Framework.Physics;
using OGame = osu.Framework.Game;

namespace CirclesGalore.Game
{
    public class CirclesGaloreGame : OGame
    {
        private RigidBodySimulation sim;
        private Box box;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = sim = new RigidBodySimulation { RelativeSizeAxes = Axes.Both };

            RigidBodyContainer<Drawable> rbc = new RigidBodyContainer<Drawable>
            {
                Child = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(150, 150),
                    Colour = Color4.Tomato,
                },
                Position = new Vector2(500, 500),
                Size = new Vector2(200, 200),
                Rotation = 45,
                Colour = Color4.Tomato,
                Masking = true,
            };

            Add(box = new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(150, 150),
                Colour = Color4.Aqua
            });

            sim.Add(rbc);
        }

        protected override void Update()
        {
            base.Update();
            box.Rotation += (float)Time.Elapsed / 10;
        }
    }
}
