namespace UnityEngine.EventSystems
{
    /// <summary>
    /// Base behaviour that has protected implementations of Unity lifecycle functions.
    /// </summary>
    public abstract class UIBehaviour : MonoBehaviour
    {
        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// field to store the UIBehaviours associated EventSystemGroup.
        /// </summary>
        private EventSystemGroup _eventSystemGroup = null;

        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// field to store and cache EventSystem instance used by this UIBehaviour.
        /// </summary>
        private EventSystem _eventSystem = null;
        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// gets the UIBehaviours associated EventSystem.
        /// </summary>
        public virtual EventSystem EventSystem
        {
            get
            {
                if (_eventSystem == null
                    || _eventSystem.EventSystemID != this.GetEventSystemID())
                    _eventSystem = EventSystem.GetSystem(this.GetEventSystemID());
                return _eventSystem;
            }
        }

        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// gets the UIBehaviours associated EventSystemID.
        /// </summary>
        public virtual int EventSystemID
        { get { return this.GetEventSystemID(); } }

        protected virtual void Awake()
        {}

        protected virtual void OnEnable()
        {
            // DOC-HINT :::::: multi event system support
            // updates the UIBehaviours EventSystemGroup.
            this.UpdateEventSystemGroup();
        }

        protected virtual void Start()
        {}

        protected virtual void OnDisable()
        {}

        protected virtual void OnDestroy()
        {}

        /// <summary>
        /// Returns true if the GameObject and the Component are active.
        /// </summary>
        public virtual bool IsActive()
        {
            return isActiveAndEnabled;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {}

        protected virtual void Reset()
        {}
#endif
        /// <summary>
        /// This callback is called if an associated RectTransform has its dimensions changed. The call is also made to all child rect transforms, even if the child transform itself doesn't change - as it could have, depending on its anchoring.
        /// </summary>
        protected virtual void OnRectTransformDimensionsChange()
        {}

        protected virtual void OnBeforeTransformParentChanged()
        {}

        protected virtual void OnTransformParentChanged()
        {
            // DOC-HINT :::::: multi event system support
            // updates the UIBehaviours EventSystemGroup.
            this.UpdateEventSystemGroup();
        }

        protected virtual void OnDidApplyAnimationProperties()
        {}

        protected virtual void OnCanvasGroupChanged()
        {}

        /// <summary>
        /// Called when the state of the parent Canvas is changed.
        /// </summary>
        protected virtual void OnCanvasHierarchyChanged()
        {}

        /// <summary>
        /// Returns true if the native representation of the behaviour has been destroyed.
        /// </summary>
        /// <remarks>
        /// When a parent canvas is either enabled, disabled or a nested canvas's OverrideSorting is changed this function is called. You can for example use this to modify objects below a canvas that may depend on a parent canvas - for example, if a canvas is disabled you may want to halt some processing of a UI element.
        /// </remarks>
        public bool IsDestroyed()
        {
            // Workaround for Unity native side of the object
            // having been destroyed but accessing via interface
            // won't call the overloaded ==
            return this == null;
        }

        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// updates the EventSystemGroup the current UIBehaviour belongs to.
        /// </summary>
        /// <remarks>searching on the current GameObject first and the hierachy upwards second</remarks>
        private void UpdateEventSystemGroup()
        {
            _eventSystemGroup = this.GetComponent<EventSystemGroup>();
            if (_eventSystemGroup == null)
                _eventSystemGroup = this.GetComponentInParent<EventSystemGroup>();
        }

        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// checks if the UIBahaviour is associated with the same EventSystem as the eventData.
        /// </summary>
        /// <param name="eventData">event data which should be checked for a matching EventSystem's id</param>
        /// <returns>true if the UIBehaviour's EventSystemID matches the event data's EventSystemID, false otherwise</returns>
        protected virtual bool CompareEventSystemID(BaseEventData eventData)
        {
            return this.EventSystemID == eventData.EventSystemID;
        }

        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// checks if the UIBahaviour is associated with the same EventSystem as the other UIBehaviour.
        /// </summary>
        /// <param name="other">other UIBehaviour which should be checked for a matching EventSystem's id</param>
        /// <returns>true if the UIBehaviour's EventSystemID matches the other UIBehaviour's EventSystemID, false otherwise</returns>
        protected virtual bool CompareEventSystemID(UIBehaviour other)
        {
            return this.EventSystemID == other.EventSystemID;
        }

        // DOC-HINT :::::: multi event system support
        /// <summary>
        /// gets the id of the associated EventSystem.
        /// </summary>
        /// <returns>returns the id of the associated EventSystem or otherwise zero as the default</returns>
        protected virtual int GetEventSystemID()
        {
            return (_eventSystemGroup != null) ? _eventSystemGroup.EventSystemID : 0;
        }
    }
}
