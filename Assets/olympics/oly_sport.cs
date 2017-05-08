using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oly_sport : MonoBehaviour {

	[SerializeField] oly_manager manager;
	[SerializeField] Sprite[] flags;
	[SerializeField] GameObject aprefab;

	string[] countries = {"AFGHANISTAN", "ALBANIA", "ALGERIA", "AMERICAN SAMOA", "ANDORRA", "ANGOLA", "ANTIGUA AND BARBUDA",
		"ARGENTINA", "ARMENIA", "ARUBA", "AUSTRALIA", "AUSTRIA", "AZERBAIJAN", "BAHAMAS", "BAHRAIN", "BANGLADESH", "BARBADOS",
		"BELARUS", "BELGIUM", "BELIZE", "BENIN", "BERMUDA", "BHUTAN", "BOLIVIA", "BOSNIA AND HERZEGOVINA", "BOTSWANA", "BRAZIL",
		"BRUNEI DARUSSALAM", "BULGARIA", "BURKINA FASO", "BURUNDI", "CAMBODIA", "CAMEROON", "CANADA", "CAPE VERDE", "CAYMAN ISLANDS",
		"CENTRAL AFRICAN REPUBLIC", "CHAD", "CHILE", "CHINESE TAIPEI", "COLOMBIA", "COMOROS", "CONGO", "COOK ISLANDS", "COSTA RICA",
		"CÔTE D'IVOIRE", "CROATIA", "CUBA", "CYPRUS", "CZECH REPUBLIC", "DEMOCRATIC PEOPLE'S REPUBLIC OF KOREA", "DEMOCRATIC REPUBLIC OF THE CONGO",
		"DENMARK", "DJIBOUTI", "DOMINICA", "DOMINICAN REPUBLIC", "ECUADOR", "EGYPT", "EL SALVADOR", "EQUATORIAL GUINEA", "ERITREA", "ESTONIA", "ÉTHIOPIE",
		"FEDERATED STATES OF MICRONESIA", "FIJI", "FINLAND", "FRANCE", "GABON", "GAMBIA", "GEORGIA", "GERMANY", "GHANA", "GREAT BRITAIN", "GREECE",
		"GRENADA", "GUAM", "GUATEMALA", "GUINEA", "GUINEA-BISSAU", "GUYANA", "HAITI", "HONDURAS", "HONG-KONG", "HUNGARY", "ICELAND", "ÎLES MARSHALL", "INDIA",
		"INDONESIA", "IRAQ", "IRELAND", "ISLAMIC REPUBLIC OF IRAN", "ISRAEL", "ITALY", "JAMAICA", "JAPAN", "JORDAN", "KAZAKHSTAN", "KENYA", "KIRIBATI", "KOSOVO",
		"KUWAIT", "KYRGYZSTAN", "LAO PEOPLE'S DEMOCRATIC REPUBLIC", "LATVIA", "LEBANON", "LESOTHO", "LIBERIA", "LIBYA", "LIECHTENSTEIN", "LITHUANIA", "LUXEMBOURG",
		"MADAGASCAR", "MALAWI", "MALAYSIA", "MALDIVES", "MALI", "MALTA", "MAURITANIA", "MAURITIUS", "MEXICO", "MONACO", "MONGOLIA", "MONTENEGRO", "MOROCCO", "MOZAMBIQUE",
		"MYANMAR", "NAMIBIA", "NAURU", "NEPAL", "NETHERLANDS", "NEW ZEALAND", "NICARAGUA", "NIGER", "NIGERIA", "NORWAY", "OMAN", "PAKISTAN", "PALAU", "PALESTINE", "PANAMA",
		"PAPUA NEW GUINEA", "PARAGUAY", "PEOPLE'S REPUBLIC OF CHINA", "PERU", "PHILIPPINES", "POLAND", "PORTUGAL", "PUERTO RICO", "QATAR", "REPUBLIC OF KOREA",
		"REPUBLIC OF MOLDOVA", "ROMANIA", "RUSSIAN FEDERATION", "RWANDA", "SAINT KITTS AND NEVIS", "SAINT LUCIA", "SAMOA (UNTIL 1996 WESTERN SAMOA)", "SAN MARINO",
		"SAO TOME AND PRINCIPE", "SAUDI ARABIA", "SENEGAL", "SERBIA", "SEYCHELLES", "SIERRA LEONE", "SINGAPORE", "SLOVAKIA", "SLOVENIA", "SOLOMON ISLANDS", "SOMALIA",
		"SOUTH AFRICA", "SOUTH SUDAN", "SPAIN", "SRI LANKA", "ST VINCENT AND THE GRENADINES", "SUDAN", "SURINAME", "SWAZILAND", "SWEDEN", "SWITZERLAND", "SYRIAN ARAB REPUBLIC",
		"TAJIKISTAN", "THAILAND", "THE FORMER YUGOSLAV REPUBLIC OF MACEDONIA", "TIMOR-LESTE", "TOGO", "TONGA", "TRINIDAD AND TOBAGO", "TUNISIA", "TURKEY", "TURKMENISTAN",
		"TUVALU", "UGANDA", "UKRAINE", "UNITED ARAB EMIRATES", "UNITED REPUBLIC OF TANZANIA", "UNITED STATES OF AMERICA", "URUGUAY", "UZBEKISTAN", "VANUATU", "VENEZUELA",
		"VIETNAM", "VIRGIN ISLANDS BRITISH", "VIRGIN ISLANDS US", "YEMEN", "ZAMBIA", "ZIMBABWE"
	};

	int endpoint = 11;

	GameObject[] athletes = new GameObject[5];

	int[] a = new int[5]; //keeps track of which countries are at play
	float[] speeds = new float[5];

	int winner;
	bool done;

	int maxspeed;

	bool going;

	// Use this for initialization
	void Start () {
		done = false;
		winner = Random.Range (0, athletes.Length);
		for (int i = 0; i < athletes.Length; i++) {
			a [i] = Random.Range (0, flags.Length);
			athletes [i] = Instantiate (aprefab, new Vector3 (-13.5f,3.5f-i*1.85f,6f), Quaternion.identity);
			athletes [i].GetComponent<SpriteRenderer> ().sprite = flags [a[i]];
			athletes [i].transform.parent = this.transform;
			athletes [i].transform.GetChild (0).GetComponent<Animator> ().Play ("still");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (going) {
			bool alldone = true;
			for (int i = 0; i < athletes.Length; i++) {
				Vector3 t = athletes [i].transform.position;
				t.x += Random.Range (0.01f, 0.1f);
				if (t.x > endpoint && !done) {
					done = true;
					winner = i;
					athletes [i].transform.position = t;
				} else if (t.x < endpoint) {
					athletes [i].transform.position = t;
					alldone = false;
				}
			}
			if (alldone) {
				manager.endRace ();
//				going = false;
				setGoing(false);
			}
		}
	}

	public void setGoing(bool g){
		if (!going && g) {
			for (int i = 0; i < athletes.Length; i++) {
				athletes [i].transform.GetChild (0).GetComponent<Animator> ().Play ("running");
			}
		} else if (going && !g) {
			for (int i = 0; i < athletes.Length; i++) {
				athletes [i].transform.GetChild (0).GetComponent<Animator> ().Play ("still");
			}
		}
		going = g;
	}

	public int getwinner(){
		return winner;
	}

	public GameObject[] ath(){
		return athletes;
	}
}
