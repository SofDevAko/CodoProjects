# Inside models.py
from __future__ import unicode_literals
from django.db import models
import re, bcrypt
from ..imdb.models import *
# Create your models here.
EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
NAME_REGEX = re.compile(r'^[a-zA-Z]+$')


class UserManager(models.Manager):
    def validate(self, post_data):
        errors = []
        if len(post_data['first_name']) < 2:
            errors.append("First name is too short!")
        if len(post_data['last_name']) < 2:
            errors.append("Last name is too short!")
        if len(post_data['password']) < 8:
            errors.append("Password is too short!")
        if (post_data['password']!=post_data['pswdcon']):
            errors.append("Passwords don't match!")    
        if not NAME_REGEX.match((post_data['first_name'])):
            errors.append("Invalid first name!")    
        if not NAME_REGEX.match((post_data['last_name'])):
            errors.append("Invalid last name!")
        if not EMAIL_REGEX.match((post_data['email'])):
            errors.append("Invalid Email!")
        if (Users.objects.filter(email=post_data['email']).count()>0):
            errors.append("Email already exists!")
        if not len(errors):
            bpass = bcrypt.hashpw(post_data['password'].encode(), bcrypt.gensalt())
            Users.objects.create(first_name=post_data['first_name'],last_name=post_data['last_name'],email=post_data['email'],password=bpass)
        return errors
    
    def logval(self, post_data):
        errors = []
        if not EMAIL_REGEX.match((post_data['email_login'])):
            errors.append("Invalid Email!")
        elif (Users.objects.filter(email=post_data['email_login']).count()<1):
            errors.append("Incorrect Email!")
        elif not (bcrypt.checkpw(post_data['password_login'].encode(),((Users.objects.filter(email=post_data['email_login']))[0].password).encode())==True):
            errors.append("Incorrect Password!")
        return errors  

    def updateval(self, post_data):
        errors = []
        if len(post_data['first_name']) < 2:
            errors.append("First name is too short!")
        if len(post_data['last_name']) < 2:
            errors.append("Last name is too short!")
        if not NAME_REGEX.match((post_data['first_name'])):
            errors.append("Invalid first name!")    
        if not NAME_REGEX.match((post_data['last_name'])):
            errors.append("Invalid last name!")
        if not EMAIL_REGEX.match((post_data['email'])):
            errors.append("Invalid Email!")
        return errors        

class Users(models.Model):
    first_name = models.CharField(max_length=255)
    last_name = models.CharField(max_length=255)
    email = models.CharField(max_length=255)
    password = models.CharField(max_length=255)
    created_at = models.DateTimeField(auto_now_add = True)
    updated_at = models.DateTimeField(auto_now = True)
    # Preference section
    #Adventure 12
    Adventure = models.IntegerField(default = 1)
    #Fantasy 14
    Fantasy = models.IntegerField(default = 1)
    #Comedy 35
    Comedy = models.IntegerField(default = 1)
    #Romance 10749
    Romance = models.IntegerField(default = 1)
    #Crime 80
    Crime = models.IntegerField(default = 1)
    #Horror 27
    Horror = models.IntegerField(default = 1)
    #Thriller 53
    Thriller = models.IntegerField(default = 1)
    #Drama 18
    Drama = models.IntegerField(default = 1)
    #SciFi 878
    SciFi = models.IntegerField(default = 1)
    #Action 28
    Action = models.IntegerField(default = 1)
    #Mystery 9648
    Mystery = models.IntegerField(default = 1)
    #Animation 16
    Animation = models.IntegerField(default = 1)
    #Family 10751
    Family = models.IntegerField(default = 1)
    #Western 37
    Western = models.IntegerField(default = 1)
    pref = [Adventure,Fantasy,Comedy,Romance,Crime,Horror,Thriller,Drama,SciFi,Action,Mystery,Animation,Family,Western]
    # End of Preference
    objects = UserManager()
