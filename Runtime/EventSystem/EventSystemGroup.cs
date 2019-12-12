namespace UnityEngine.EventSystems
{

    /* =======================
     * change-log:     
	 * [2017/01/04]
	 *		updated sources to 5.5.0       
     * [2015/06/09]
     *      updated sources from 4.6 branch to 5.0.2f 
     * [2015/03/24]
     *      added comments to the multi event system support code
     *      added multi event system support (affected code is marked with "DOC-HINT :::::: multi event system support" comment)
    */

    // DOC-HINT :::::: multi event system support
    /// <summary>
    /// provides a component to associate interface elements to a specific EventSystem using simple numerical identifier
    /// </summary>
    public sealed class EventSystemGroup : MonoBehaviour
    {
        /// <summary>
        /// private field to store the id of EventSystem all contained interface elements should be associated with
        /// </summary>
        [SerializeField()]
        private int _eventSystemID = 0;

        /// <summary>
        /// gets or sets the eventsystem's id
        /// </summary>
        public int EventSystemID
        {
            get { return _eventSystemID; }
            set { _eventSystemID = value; }
        }

    }
}
