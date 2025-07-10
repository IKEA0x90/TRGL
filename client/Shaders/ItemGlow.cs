using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace TRGLC.Shaders {
    class ItemGlow : ShaderEffect {

        private static readonly PixelShader _pixelShader = new PixelShader {
            UriSource = new Uri("/TRGLC;component/Shaders/ItemGlow.ps", UriKind.Relative)
        };

        public ItemGlow() {
            this.PixelShader = _pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(OutlineColorProperty);
            UpdateShaderValue(GlowColorProperty);
            UpdateShaderValue(ThresholdProperty);
            UpdateShaderValue(GlowStrengthProperty);
            UpdateShaderValue(GlowRadiusProperty);
        }

        // Main texture (auto-handled)
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ItemGlow), 0);

        // The outline color to match (register c0)
        public static readonly DependencyProperty OutlineColorProperty =
            DependencyProperty.Register("OutlineColor", typeof(Color), typeof(ItemGlow),
                new UIPropertyMetadata(Colors.Red, PixelShaderConstantCallback(0)));

        public Color OutlineColor {
            get => (Color)GetValue(OutlineColorProperty);
            set => SetValue(OutlineColorProperty, value);
        }

        // The color to use for the glow (register c1)
        public static readonly DependencyProperty GlowColorProperty =
            DependencyProperty.Register("GlowColor", typeof(Color), typeof(ItemGlow),
                new UIPropertyMetadata(Colors.Red, PixelShaderConstantCallback(1)));

        public Color GlowColor {
            get => (Color)GetValue(GlowColorProperty);
            set => SetValue(GlowColorProperty, value);
        }

        // Tolerance for matching the outline color (register c2)
        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(float), typeof(ItemGlow),
                new UIPropertyMetadata(0.1f, PixelShaderConstantCallback(2)));

        public float Threshold {
            get => (float)GetValue(ThresholdProperty);
            set => SetValue(ThresholdProperty, value);
        }

        // Glow intensity (register c3)
        public static readonly DependencyProperty GlowStrengthProperty =
            DependencyProperty.Register("GlowStrength", typeof(float), typeof(ItemGlow),
                new UIPropertyMetadata(1.0f, PixelShaderConstantCallback(3)));

        public float GlowStrength {
            get => (float)GetValue(GlowStrengthProperty);
            set => SetValue(GlowStrengthProperty, value);
        }

        // Glow blur radius (register c4)
        public static readonly DependencyProperty GlowRadiusProperty =
            DependencyProperty.Register("GlowRadius", typeof(float), typeof(ItemGlow),
                new UIPropertyMetadata(0.01f, PixelShaderConstantCallback(4)));

        public float GlowRadius {
            get => (float)GetValue(GlowRadiusProperty);
            set => SetValue(GlowRadiusProperty, value);
        }

    }
}
