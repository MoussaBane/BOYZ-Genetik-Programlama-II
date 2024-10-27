using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain_sc : MonoBehaviour
{
    public int DNALength = 1;
    public float timeAlive;
    public DNA_sc dna_sc;
    private ThirdPersonCharacter m_Character;
    private Vector3 m_Move;
    private bool m_Jump;
    bool alive = true;

    public float distanceTravelled;
    Vector3 startPosition;

    // Collision detection for death
    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    // Initialize DNA and components
    public void Init()
    {
        dna_sc = new DNA_sc(DNALength, 6);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
        startPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        bool crouch = false;

        // Interpret DNA values for movement
        if (dna_sc.GetInt(0) == 0) v = 1;
        else if (dna_sc.GetInt(0) == 1) v = -1;
        else if (dna_sc.GetInt(0) == 2) h = -1;
        else if (dna_sc.GetInt(0) == 3) h = 1;
        else if (dna_sc.GetInt(0) == 4) m_Jump = true;
        else if (dna_sc.GetInt(0) == 5) crouch = true;

        m_Move = v * Vector3.forward + h * Vector3.right;
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;

        if (alive)
        {
            timeAlive += Time.deltaTime;
            distanceTravelled = Vector3.Distance(this.transform.position, startPosition);
        }
    }
}
