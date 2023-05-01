using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace LDJam53.Graphics;

using CoreNumerics = System.Numerics;
using CoreDrawing = System.Drawing;

public class MonoAudioDriver : AudioDriver {
    class SoundInstance {
        public Object reference;
        public MonoGameSound file;
        public SoundEffectInstance instance;
        public Vector2? position;
    }

    private readonly SpriteBatch _spriteBatch;
    private readonly ContentManager _contentManager;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly HashSet<WeakReference<MonoGameMusic>> _music = new();
    private readonly HashSet<WeakReference<MonoGameSound>> _sounds = new();

    private MonoGameMusic? _activeMusic;
    private List<SoundInstance> _soundInstances = new();

    public MonoAudioDriver(ContentManager contentManager) {
        _contentManager = contentManager;
    }

    public Music GetMusic(String name) {
        var music = _music.ToArray();
        foreach (var musicRef in music) {
            if (musicRef.TryGetTarget(out var t)) {
                if (t.Name == name) {
                    return t;
                }
            }
            else {
                _music.Remove(musicRef);
            }
        }

        var musicInst = new MonoGameMusic(_contentManager.Load<Song>(name));
        _music.Add(new(musicInst));

        return musicInst;
    }

    public Sound GetSound(String name) {
        var sounds = _sounds.ToArray();
        foreach (var soundRef in sounds) {
            if (soundRef.TryGetTarget(out var t)) {
                if (t.Name == name) {
                    return t;
                }
            }
            else {
                _sounds.Remove(soundRef);
            }
        }

        var sound = new MonoGameSound(_contentManager.Load<SoundEffect>(name));
        _sounds.Add(new(sound));

        return sound;
    }

    public void Play(Music music, Single volume, Boolean loop) {
        if (music is not MonoGameMusic monoMusic) throw new Exception("Unsupported music format");

        if (_activeMusic != monoMusic || MediaPlayer.State != MediaState.Playing) {
            _activeMusic = monoMusic;
            MediaPlayer.Play(monoMusic.Song);
        }
        if (MediaPlayer.Volume != volume) {
            MediaPlayer.Volume = volume;
        }
    }

    public void Play(Sound sound, Single volume, CoreNumerics.Vector2? position = null, Object? instance = null) {
        if (sound is not MonoGameSound monoSound) throw new Exception("Unsupported sound format");

        instance ??= new();

        var soundInstance = _soundInstances.FirstOrDefault(p => p.reference == instance && p.file == sound);
        if (soundInstance == null) {
            soundInstance = new SoundInstance {
                file = monoSound,
                reference = instance,
                instance = monoSound.Sound.CreateInstance()
            };
            _soundInstances.Add(soundInstance);
        }

        if (soundInstance.instance.State != SoundState.Playing) {
            soundInstance.instance.Play();
        }
    }
}
