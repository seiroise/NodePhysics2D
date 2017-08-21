using UnityEngine;

public class FPSCounter : MonoBehaviour {

	[SerializeField, Range(1, 60)]
	int _frameRange = 60;

	int _averageFps;
	int _highestFps;
	int _lowestFps;

	int[] _fpsBuffer;
	int _fpsBufferIndex;

	public int averageFps {
		get {
			return _averageFps;
		}
	}
	public int highestFps {
		get {
			return _highestFps;
		}
	}
	public int lowestFps {
		get {
			return _lowestFps;
		}
	}

	void Update() {
		if (_fpsBuffer == null || _fpsBuffer.Length != _frameRange) {
			InitializeBuffer();
		}
		UpdateBuffer();
		CalculateFPS();
	}

	void InitializeBuffer() {
		_fpsBuffer = new int[_frameRange];
		_fpsBufferIndex = 0;
	}

	void UpdateBuffer() {
		_fpsBuffer[_fpsBufferIndex] = (int)(1f / Time.unscaledDeltaTime);
		_fpsBufferIndex = (_fpsBufferIndex + 1) % _fpsBuffer.Length;
	}

	void CalculateFPS() {
		int sum = 0;
		int highest = 0;
		int lowest = int.MaxValue;
		for (int i = 0; i < _fpsBuffer.Length; ++i) {
			int fps = _fpsBuffer[i];
			sum += fps;
			if (fps > highest) {
				highest = fps;
			}
			if (fps < lowest) {
				lowest = fps;
			}
		}
		_averageFps = sum / _fpsBuffer.Length;
		_highestFps = highest;
		_lowestFps = lowest;
	}
}