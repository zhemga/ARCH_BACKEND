﻿namespace VIRTUAL_LAB_API.Model
{
public class EducationalMaterial
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> CloudDriveAttachedFileURLs { get; set; }

    public Course Course { get; set; }

}
}
