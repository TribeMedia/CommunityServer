﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLimit.CloudComputing.SharpBox
{
    /// <summary>
    /// This interface defines all methods which has to be implemented 
    /// to support asynchoniously I/O, e.g. for WP7
    /// </summary>
    public interface ICloudStorageAsyncInterface
    {
        /// <summary>
        /// The BeginOpenRequest-Method starts the authentication process of a given user with the
        /// configured cloud storage service. After finishing this process in the background
        /// the callback delegate will be executed. Please use EndOpenRequest in the callback 
        /// delegate
        /// </summary>
        /// <param name="callback">Delegate which will be called when the background action is finished</param>
        /// <param name="configuration">The configuration information of the cloud storage service</param>
        /// <param name="token">The credential token of the user which has to be authenticated</param>
        /// <returns>The asyncresult is the entry point to all generated results</returns>        
        IAsyncResult BeginOpenRequest(AsyncCallback callback, ICloudStorageConfiguration configuration, ICloudStorageAccessToken token);

        /// <summary>
        /// The EndOpenRequest method will finish a started async call. Please use this
        /// method in the callback delegate.
        /// </summary>
        /// <param name="asyncResult">The result reference generated by BeginOpenRequest</param>
        /// <returns>The generated cloud access token</returns>
        ICloudStorageAccessToken EndOpenRequest(IAsyncResult asyncResult);  
        
        /// <summary>
        /// The BeginGetChildsRequest-Method starts retrieving the list of childs of a given
        /// parent container. This procedure can take a while if some information has to 
        /// be requested from the network
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        IAsyncResult BeginGetChildsRequest(AsyncCallback callback, ICloudDirectoryEntry parent);      

        /// <summary>
        /// The EndGetChildsRequest-Method will finish a started async call. Please use this
        /// method in the callback delegate.
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        List<ICloudFileSystemEntry> EndGetChildsRequest(IAsyncResult asyncResult);

        /// <summary>
        /// The begin routine the receive the root element from cloud storage
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        IAsyncResult BeginGetRootRequest(AsyncCallback callback);

        /// <summary>
        /// The end routine the receive the root element
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        ICloudDirectoryEntry EndGetRootRequest(IAsyncResult asyncResult);        
    }
}
