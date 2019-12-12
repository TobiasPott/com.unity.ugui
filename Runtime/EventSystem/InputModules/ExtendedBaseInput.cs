using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
    /// <summary>
    /// Interface to the Input system used by the BaseInputModule. With this it is possible to bypass the Input system with your own but still use the same InputModule. For example this can be used to feed fake input into the UI or interface with a different input system.
    /// </summary>
    [RequireComponent(typeof(BaseInputModule))]
    public class ExtendedBaseInput : BaseInput
    {
        [SerializeField()]
        protected bool _enableMouseInput = true;

        public bool EnableMouseInput
        {
            get { return _enableMouseInput; }
            set { _enableMouseInput = false; }
        }

        protected override void Awake()
        {
            BaseInputModule inputModule = this.GetComponent<BaseInputModule>();
            inputModule.inputOverride = this;
        }

        /// <summary>
        /// Interface to Input.mousePresent. Can be overridden to provide custom input instead of using the Input class.
        /// </summary>
        public override bool mousePresent
        {
            get { return _enableMouseInput && Input.mousePresent; }
        }

        /// <summary>
        /// Interface to Input.GetMouseButtonDown. Can be overridden to provide custom input instead of using the Input class.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public override bool GetMouseButtonDown(int button)
        {
            return _enableMouseInput && Input.GetMouseButtonDown(button);
        }

        /// <summary>
        /// Interface to Input.GetMouseButtonUp. Can be overridden to provide custom input instead of using the Input class.
        /// </summary>
        public override bool GetMouseButtonUp(int button)
        {
            return _enableMouseInput && Input.GetMouseButtonUp(button);
        }

        /// <summary>
        /// Interface to Input.GetMouseButton. Can be overridden to provide custom input instead of using the Input class.
        /// </summary>
        public override bool GetMouseButton(int button)
        {
            return _enableMouseInput && Input.GetMouseButton(button);
        }

        /// <summary>
        /// Interface to Input.mousePosition. Can be overridden to provide custom input instead of using the Input class.
        /// </summary>
        public override Vector2 mousePosition
        {
            get
            {
                if (_enableMouseInput)
                    return MultipleDisplayUtilities.GetMousePositionRelativeToMainDisplayResolution();
                return Vector2.zero;
            }
        }

        /// <summary>
        /// Interface to Input.mouseScrollDelta. Can be overridden to provide custom input instead of using the Input class.
        /// </summary>
        public override Vector2 mouseScrollDelta
        {
            get
            {
                if (_enableMouseInput)
                    return Input.mouseScrollDelta;
                return Vector2.zero;
            }
        }

    }
}
