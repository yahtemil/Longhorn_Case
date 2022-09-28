using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Paint : MonoBehaviour
{
    public Board board;
    [Range(2, 512)]
    [SerializeField] private int _textureSize = 128;
    [SerializeField] private TextureWrapMode _textureWrapMode;
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Material _material;

    [SerializeField] private Camera _camera;
    [SerializeField] private Collider _collider;
    [SerializeField] private Color _color;
    [SerializeField] private int _brushSize = 8;

    float paintCounter;
    float totalPaintValue;

    private int _oldRayX, _oldRayY;

    void Awake()
    {
        if (_texture == null)
        {
            _texture = new Texture2D(_textureSize, _textureSize);
        }
        if (_texture.width != _textureSize)
        {
            _texture.Resize(_textureSize, _textureSize);
        }
        _texture.wrapMode = _textureWrapMode;
        _texture.filterMode = _filterMode;
        _material.mainTexture = _texture;
        _texture.Apply();
        totalPaintValue = (int)Mathf.Pow(_textureSize, 2);
    }

    private void Update()
    {
        _brushSize += (int)Input.mouseScrollDelta.y;

        if (Input.GetMouseButtonDown(0))
        {
            board.pencil.gameObject.transform.DOKill();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousPos = Input.mousePosition;
            mousPos.z = 1.5f;
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousPos);
            board.pencil.gameObject.transform.DOMove(pos, 0.1f);
            board.pencil.gameObject.transform.DORotate(Vector3.zero, 0.1f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            RaycastHit hit;
            if (_collider.Raycast(ray, out hit, 100f))
            {

                int rayX = (int)(hit.textureCoord.x * _textureSize);
                int rayY = (int)(hit.textureCoord.y * _textureSize);

                if (_oldRayX != rayX || _oldRayY != rayY)
                {
                    //DrawQuad(rayX, rayY);
                    DrawCircle(rayX, rayY);
                    _oldRayX = rayX;
                    _oldRayY = rayY;
                }
                _texture.Apply();
            }
            if (paintCounter / totalPaintValue >= 0.98f)
            {
                board.NextEvent();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            board.pencil.gameObject.transform.DOMove(board.pencilTransform.position, 0.5f);
            board.pencil.gameObject.transform.DORotate(board.pencilTransform.localEulerAngles, 0.5f);
        }
    }

    void DrawQuad(int rayX, int rayY)
    {
        for (int y = 0; y < _brushSize; y++)
        {
            for (int x = 0; x < _brushSize; x++)
            {
                _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, _color);
            }
        }
    }

    void DrawCircle(int rayX, int rayY)
    {
        for (int y = 0; y < _brushSize; y++)
        {
            for (int x = 0; x < _brushSize; x++)
            {

                float x2 = Mathf.Pow(x - _brushSize / 2, 2);
                float y2 = Mathf.Pow(y - _brushSize / 2, 2);
                float r2 = Mathf.Pow(_brushSize / 2 - 0.5f, 2);

                if (x2 + y2 < r2)
                {
                    int pixelX = rayX + x - _brushSize / 2;
                    int pixelY = rayY + y - _brushSize / 2;

                    if (pixelX >= 0 && pixelX < _textureSize && pixelY >= 0 && pixelY < _textureSize)
                    {
                        Color oldColor = _texture.GetPixel(pixelX, pixelY);
                        if (oldColor != _color)
                        {
                            paintCounter++;
                            Color resultColor = Color.Lerp(oldColor, _color, _color.a);
                            _texture.SetPixel(pixelX, pixelY, resultColor);
                        }
                    }

                }
            }
        }

    }
}