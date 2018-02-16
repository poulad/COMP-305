using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Points
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject Point;

        public GameObject PlayButton;

        public GameObject Jetdroid;

        public int MaxPoints = 10;

        private static SceneManager _this;

        private GameObject _jetdroid;

        private int _nextPoint;

        private readonly List<GameObject> _points = new List<GameObject>();

        public void Start()
        {
            PlayButton.SetActive(false);
            _this = this;
        }

        public void FixedUpdate()
        {
            if (_nextPoint >= _points.Count)
            {
                Destroy(_jetdroid);
                _jetdroid = null;
            }

            if (_jetdroid == null)
                return;

            var nextPos = _points[_nextPoint].transform.position;
            var currPos = _jetdroid.transform.position;
            if (Mathf.Abs(currPos.x - nextPos.x) < .1 && Mathf.Abs(currPos.y - nextPos.y) < .1)
            {
                _nextPoint += 1;
            }

            var jdRigidbody = _jetdroid.GetComponent<Rigidbody2D>();
            var jdsSpriteRenderer = _jetdroid.GetComponent<SpriteRenderer>();
            jdRigidbody.velocity = new Vector2(nextPos.x - currPos.x, nextPos.y - currPos.y).normalized * 2;

            jdsSpriteRenderer.flipX = jdRigidbody.velocity.x < 0;
        }

        public void OnMouseDown()
        {
            if (_points.Count == MaxPoints)
                return;

            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            var point = Instantiate(Point, position, Quaternion.identity);
            _points.Add(point);

            if (!PlayButton.activeSelf && _points.Count == 3)
            {
                PlayButton.SetActive(true);
            }
        }

        public static void Play()
        {
            _this._nextPoint = 0;
            _this._jetdroid = Instantiate(_this.Jetdroid, _this._points.First().transform.position, Quaternion.identity);
        }

        public static void ResetPoints()
        {
            var allPoints = _this._points.ToArray();
            _this._points.Clear();

            foreach (var p in allPoints)
            {
                Destroy(p.gameObject);
            }
        }
    }
}