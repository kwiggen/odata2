# odata2
Test Of OData4 and Navigational Properties

This project uses OData v4 constructs to illustrate how to use @odata.id to set Navigation Properties during a Object Creation (or POST).

The main Object Type is a Course which has a Navigation Property to Teacher (1-1).  You can create a plain Course.
Course then has two subclasses, InPersonCourse and ExternalCourse.

In the InPersonCoures the Navigation Property points to an object that is owned by the Server (Location) and the @odata.id will create an 
actual Location property when it is created.

The ExternalCourse is supposed to represent a system where the ExternalLocation is still a Navigation Property, however the @odata.id 
points to an Entity that is not hosted by the server.

In this example I have created a new Deserializer which is used to take @odata.id objects in a POST and create the correct object as the 
default OData lib does not do this.

The other issue with the current version of odata libs is that you can't have a Complex Type which contains another Entity Type.  To work around this, this example treats all objects as Entities, yet used the Serializer to hide the ID of the ENtity which should be treated as a Complex Type.  In fact the ID is hardcoded to -1 in this example.  This example is in the ResourceWrapper and Resource objects which are part of Course

Thanks to Gareth who wrote a custom serializer to return @data.id for the ExternalCourse Location object. 
