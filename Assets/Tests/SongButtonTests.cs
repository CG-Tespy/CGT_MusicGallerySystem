using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CGT.MusicGallery;

public class SongButtonTests
{
    [SetUp]
    public virtual void SetUp()
    {
        string pathToScene = "TestScenes/SongButtonTests";
        scenePrefab = Resources.Load<GameObject>(pathToScene);
        scene = Object.Instantiate(scenePrefab);
        songButton = scene.GetComponentInChildren<SongButtonController>();
    }

    protected GameObject scenePrefab, scene;
    protected SongButtonController songButton;

    [UnityTest]
    [Ignore("")]
    public virtual IEnumerator PlaysSongAssigned()
    {
        yield return new System.NotImplementedException();
    }

    [UnityTest]
    [Ignore("")]
    public virtual IEnumerator DisplaysRightSongName()
    {
        // Might be best to have this assigned to View classes
        yield return new System.NotImplementedException();
    }

    [TearDown]
    public virtual void TearDown()
    {
        Object.Destroy(scene.gameObject);
    }
}
