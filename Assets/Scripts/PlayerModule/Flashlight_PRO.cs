using UnityEngine;
using System.Collections;




public class Flashlight_PRO : MonoBehaviour 
{
	
	[SerializeField()] GameObject Lights; 
	[SerializeField()] AudioSource switch_sound; 
	[SerializeField()] ParticleSystem dust_particles; 



	private Light spotlight;
	private Material ambient_light_material;
	private Color ambient_mat_color;
	private bool is_enabled = false;










	void Start () 
	{
		spotlight = Lights.transform.Find ("Spotlight").GetComponent<Light> ();
		ambient_light_material = Lights.transform.Find ("ambient").GetComponent<Renderer> ().material;
		ambient_mat_color = ambient_light_material.GetColor ("_TintColor");
	}
	





	public void Change_Intensivity(float percentage)
	{
		percentage = Mathf.Clamp (percentage, 0, 100);


		spotlight.intensity = (8 * percentage) / 100;

		ambient_light_material.SetColor ("_TintColor", new Color(ambient_mat_color.r , ambient_mat_color.g , ambient_mat_color.b , percentage/2000)); 
	}




	
	public void Switch()
	{
		is_enabled = !is_enabled; 

		Lights.SetActive (is_enabled);

		if (switch_sound != null)
			switch_sound.Play ();
	}






	public void Enable_Particles(bool value)
	{
		if(dust_particles != null)
		{
			if(value)
			{
				dust_particles.gameObject.SetActive(true);
				dust_particles.Play();
			}
			else
			{
				dust_particles.Stop();
				dust_particles.gameObject.SetActive(false);
			}
		}
	}


}
