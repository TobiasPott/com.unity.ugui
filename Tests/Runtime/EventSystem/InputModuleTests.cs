using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class InputModuleTests
{
    Camera m_Camera;
    EventSystem m_EventSystem;
    FakeBaseInput m_FakeBaseInput;
    StandaloneInputModule m_StandaloneInputModule;
    Canvas m_Canvas;
    Image m_Image;

    [SetUp]
    public void TestSetup()
    {
        // Camera | Canvas (Image) | Event System

        m_Camera = new GameObject("Camera").AddComponent<Camera>();
        m_Camera.transform.LookAt(Vector3.forward);

        m_Canvas = new GameObject("Canvas").AddComponent<Canvas>();
        m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        m_Canvas.worldCamera = m_Camera;
        m_Canvas.gameObject.AddComponent<GraphicRaycaster>();

        m_Image = new GameObject("Image").AddComponent<Image>();
        m_Image.gameObject.transform.SetParent(m_Canvas.transform);
        RectTransform imageRectTransform = m_Image.GetComponent<RectTransform>();
        imageRectTransform.localScale = new Vector3(500f, 500f, 500f);
        imageRectTransform.localPosition = Vector3.zero;

        GameObject go = new GameObject("Event System");
        m_StandaloneInputModule = go.AddComponent<StandaloneInputModule>();
        m_FakeBaseInput = go.AddComponent<FakeBaseInput>();

        // Override input with FakeBaseInput so we can send fake mouse/keyboards button presses and touches
        m_StandaloneInputModule.inputOverride = m_FakeBaseInput;

        m_EventSystem = go.AddComponent<EventSystem>();
        m_EventSystem.pixelDragThreshold = 1;

        Cursor.lockState = CursorLockMode.None;
    }

    [UnityTest]
    public IEnumerator DragCallbacksDoGetCalled()
    {
        // While left mouse button is pressed and the mouse is moving, OnBeginDrag and OnDrag callbacks should be called
        // Then when the left mouse button is released, OnEndDrag callback should be called

        // Add script to EventSystem to update the mouse position
        m_EventSystem.gameObject.AddComponent<MouseUpdate>();

        // Add script to Image which implements OnBeginDrag, OnDrag & OnEndDrag callbacks
        DragCallbackCheck callbackCheck = m_Image.gameObject.AddComponent<DragCallbackCheck>();

        // Setting required input.mousePresent to fake mouse presence
        m_FakeBaseInput.MousePresent = true;

        yield return null;

        // Left mouse button down simulation
        m_FakeBaseInput.MouseButtonDown[0] = true;

        yield return null;

        // Left mouse button down flag needs to reset in the next frame
        m_FakeBaseInput.MouseButtonDown[0] = false;

        yield return null;

        // Left mouse button up simulation
        m_FakeBaseInput.MouseButtonUp[0] = true;

        yield return null;

        // Left mouse button up flag needs to reset in the next frame
        m_FakeBaseInput.MouseButtonUp[0] = false;

        yield return null;

        Assert.IsTrue(callbackCheck.onBeginDragCalled, "OnBeginDrag not called");
        Assert.IsTrue(callbackCheck.onDragCalled, "OnDragCalled not called");
        Assert.IsTrue(callbackCheck.onEndDragCalled, "OnEndDragCalled not called");
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(m_Camera.gameObject);
        GameObject.DestroyImmediate(m_EventSystem.gameObject);
        GameObject.DestroyImmediate(m_Canvas.gameObject);
    }
}
