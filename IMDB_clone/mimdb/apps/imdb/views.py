# -*- coding: utf-8 -*-
from __future__ import unicode_literals
from django.utils.crypto import get_random_string
from django.shortcuts import render, HttpResponse, redirect
from django.contrib import messages
from .models import *
from ..login.models import *
import bcrypt, random
import requests, json
from django.core import serializers
import matplotlib
import matplotlib.pyplot as plt


def index(request):
    url = "https://api.themoviedb.org/3/movie/now_playing?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1&region=us"
    strcurmovies = requests.get(url).content
    curmovies = json.loads(strcurmovies)
    url2 = "https://api.themoviedb.org/3/movie/top_rated?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1"
    strtopmovies = requests.get(url2).content
    topmovies =  json.loads(strtopmovies)
    url3 = "https://api.themoviedb.org/3/movie/upcoming?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1&region=us"
    strfutmovies = requests.get(url3).content
    futmovies =  json.loads(strfutmovies)
    url4 = "https://api.themoviedb.org/3/movie/popular?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1&region=us"
    strbackgrounds = requests.get(url4).content
    backgrounds =  json.loads(strbackgrounds)
    background = random.choice(backgrounds['results'])
    if 'curuser' in request.session:
        users = Users.objects.all ()
        context = {
            "curmovies":curmovies,
            "background":background,
            "topmovies":topmovies,
            "futmovies":futmovies,
            "users":users,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser'],
        }
    else:
        context = {
            "curmovies":curmovies,
             "background":background,
            "topmovies":topmovies,
            "futmovies":futmovies,
            "reg":"reg/",
            "label":"Log In"
        }
    return render(request,'imdb/index.html', context)

def watchlist(request):
    if 'curuser' in request.session:
        rev_list={}
        mov_list=[]
        user = Users.objects.get(id=request.session['curuser'])
        reviews = Review.objects.filter(reviewer=user)
        for rev in reviews:
            url = "https://api.themoviedb.org/3/movie/" + str(rev.movie) + "?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699"
            strresponse = requests.get(url).content
            movie = [json.loads(strresponse)]
            rev_list[movie[0]["poster_path"]]=rev
        for item in user.watchlist.all():
            url = "https://api.themoviedb.org/3/movie/" + str(item.mov_id) + "?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699"
            strresponse = requests.get(url).content
            movie = [json.loads(strresponse)]
            mov_list+=movie
        
        # Creating Suggestions
        total = 28
        pref = []
        
        adv = user.Adventure
        fan = user.Fantasy
        com = user.Comedy
        rom = user.Romance
        cri = user.Crime
        hor = user.Horror
        thr = user.Thriller
        dra = user.Drama
        sci = user.SciFi
        act = user.Action
        mys = user.Mystery
        ani = user.Animation
        fam = user.Family
        wes = user.Western
        pref.append(adv)
        pref.append(fan)
        pref.append(com)
        pref.append(rom)
        pref.append(cri)
        pref.append(hor)
        pref.append(thr)
        pref.append(dra)
        pref.append(sci)
        pref.append(act)
        pref.append(mys)
        pref.append(ani)
        pref.append(fam)
        pref.append(wes)
        totpref = 0
        totsug = 0
        sug_list = []
        sug_films = []
        gen_ids = ['12','14','35','10749','80','27','53','18','878','28','9648','16','10751','37']
        for p in pref:
            totpref += p
        for pr in pref:
            pr=total*pr/totpref
            sug_list.append(pr)
        for su in sug_list:
            totsug += su
        
        for su in range(len(sug_list)):
            many = sug_list[su]
            genid = gen_ids[su]
            url = "https://api.themoviedb.org/3/genre/"+ genid +"/movies?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&include_adult=true&sort_by=id.desc"
            strresponse = requests.get(url).content
            movieJSON = [json.loads(strresponse)]
            movie = movieJSON[0]["results"]
            # print(movie[:many+1])
            for idx in range(many):
                rnd = random.choice(movie)
                sug_films.append(rnd)
                movie.remove(rnd)
        print(sug_list)
        print(totpref)
        # print(sug_films)
        print(len(sug_films))
        
        #Plotting interest chart

        # labels = 'Adventure', 'Fantasy', 'Comedy', 'Romance', 'Crime', 'Horror', 'Thriller', 'Drama', 'SciFi', 'Action', 'Mystery', 'Animation', 'Family', 'Western'
        # sizes = sug_list
        # colors = ['gold', 'yellowgreen', 'lightcoral', 'pink', 'lightskyblue', 'red', 'brown', 'orange', 'green', 'purple', 'darkgreen', 'beige', 'darkred', 'blue']
        # patches, texts = plt.pie(sizes, colors=colors, shadow=True, startangle=90)
        # plt.legend(patches, labels, loc="best")
        # plt.axis('equal')
        # plt.tight_layout()
        # plt.show()
        
        if mov_list:
            background = random.choice(mov_list)
        else:
            background = None
        context = {
            "background":background,
            "sug_list":sug_films,
            "rev_list":rev_list,
            "mov_list":mov_list,
            "user":user,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser']
        }
    else:
        context = {
            "reg":"reg/",
            "label":"Log In"
        }
        return redirect('/reg/')
    return render(request,'imdb/watchlist.html', context)

def upcoming(request):
    url3 = "https://api.themoviedb.org/3/movie/upcoming?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1&region=us"
    strfutmovies = requests.get(url3).content
    futmovies =  json.loads(strfutmovies)
    background = random.choice(futmovies['results'])
    if 'curuser' in request.session:
        users = Users.objects.all ()
        context = {
            "futmovies":futmovies,
            "background": background,
            "users":users,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser']
        }
    else:
        context = {
            "futmovies":futmovies,
            "background": background,
            "reg":"reg/",
            "label":"Log In"
        }
    return render(request, 'imdb/upcoming.html', context)

def current(request):
    url = "https://api.themoviedb.org/3/movie/now_playing?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1&region=us"
    strcurmovies = requests.get(url).content
    curmovies = json.loads(strcurmovies)
    background = random.choice(curmovies['results'])
    if 'curuser' in request.session:
        users = Users.objects.all ()
        context = {
            "curmovies":curmovies,
            "background": background,
            "users":users,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser']
        }
    else:
        context = {
            "curmovies":curmovies,
            "background": background,
            "reg":"reg/",
            "label":"Log In"
        }
    return render(request, 'imdb/current.html', context)

def toprated(request):
    url2 = "https://api.themoviedb.org/3/movie/top_rated?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1"
    strtopmovies = requests.get(url2).content
    topmovies =  json.loads(strtopmovies)
    background = random.choice(topmovies['results'])
    if 'curuser' in request.session:
        users = Users.objects.all ()
        context = {
            "topmovies":topmovies,
            "background": background,
            "users":users,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser']
        }
    else:
        context = {
            "topmovies":topmovies,
            "background": background,
            "reg":"reg/",
            "label":"Log In"
        }
    return render(request, 'imdb/toprated.html', context)

def search(request):
    error = []
    title = request.POST['search'].replace(' ', '+')
    if len(title) == 0:
        error.append("If you don't search anything, you don't get anything!")
        response = error
    else:
        url = "https://api.themoviedb.org/3/search/"+request.POST["search_option"]+"?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&query="+title+"&page=1"
        strresponse = requests.get(url).content
        response = json.loads(strresponse)
    if 'curuser' in request.session:
        context = {
            "response" : response,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser']
        }
    else:
        context = {
            "response" : response,
            "reg":"reg/",
            "label":"Log In"
        }
    
    return render(request,'imdb/search.html', context)

def add(request):
    errors = Movie.objects.movalidate(request.POST)
    if len(errors):
        for error in errors:
            messages.error(request, error)
    return redirect ('/search')

def show(request, id):
    thisone = False
    url = "https://api.themoviedb.org/3/movie/"+id+"?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699"
    strresponse = requests.get(url).content
    movie = json.loads(strresponse)
    reviews = Review.objects.filter(movie=id)
    simurl = "https://api.themoviedb.org/3/movie/"+id+"/similar?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&language=en-US&page=1"
    strsim = requests.get(simurl).content
    simmovies = json.loads(strsim)
    movie["budget"]="{:,}".format(movie["budget"])
    movie["revenue"]="{:,}".format(movie["revenue"])
    if 'curuser' in request.session:
        user = Users.objects.get(id=request.session['curuser'])
        movlist = user.watchlist.all()
        for mov in movlist:
            if movie["id"] == mov.mov_id:
                thisone = True
        context = {
            "thisone":thisone,
            "movie":movie,
            "simmovies":simmovies,
            "reviews":reviews,
            "reg":"reg/logout",
            "label":"Log Out",
            "curuser":request.session['curuser']
        }
    else:
        context = {
            "movie":movie,
            "simmovies":simmovies,
            "reviews":reviews,
            "reg":"reg/",
            "label":"Log In"
        }
    return render(request,'imdb/show.html', context)

def add_list(request, id):
    if 'curuser' in request.session:
        this_user = Users.objects.get(id=request.session['curuser'])
        if Movie.objects.filter(mov_id=id).count()<1:
            Movie.objects.create(mov_id=id)
        this_movie = Movie.objects.get(mov_id=id)
        this_user.watchlist.add(this_movie)
        
        # Preference Modification
        url = "https://api.themoviedb.org/3/movie/"+id+"?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699"
        strresponse = requests.get(url).content
        the_movie = json.loads(strresponse)
        genre = []
        for gen in the_movie["genres"]:
            if gen == "Science Fiction":
                gen = "SciFi"
            genre.append(gen["name"])
        for gen in genre:
            if gen == "Science Fiction":
                gen = "SciFi"
            temp = getattr(this_user, gen)
            temp += 1
            setattr(this_user, gen, temp)
        print("Crime: ",this_user.Crime)
        print("Action: ",this_user.Action)
        print("Comedy: ",this_user.Comedy)
        print("Thriller: ",this_user.Thriller)
        print("Horror: ",this_user.Horror)
        print("Drama: ",this_user.Drama)
        print("Romance: ",this_user.Romance)
        print("SciFi: ", this_user.SciFi)
        this_user.save()

        return redirect('/watchlist')
    else:
        return redirect('/reg/')

def rm_list(request, id):
    if 'curuser' in request.session:
        this_user = Users.objects.get(id=request.session['curuser'])
        this_movie = Movie.objects.get(mov_id=id)
        this_user.watchlist.remove(this_movie)

        # Preference Modification
        url = "https://api.themoviedb.org/3/movie/"+id+"?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699"
        strresponse = requests.get(url).content
        the_movie = json.loads(strresponse)
        genre = []
        for gen in the_movie["genres"]:
            if gen == "Science Fiction":
                gen = "SciFi"
            genre.append(gen["name"])
        for gen in genre:
            if gen == "Science Fiction":
                gen = "SciFi"
            temp = getattr(this_user, gen)
            temp -= 1
            setattr(this_user, gen, temp)
        print("Crime: ",this_user.Crime)
        print("Action: ",this_user.Action)
        print("Comedy: ",this_user.Comedy)
        print("Thriller: ",this_user.Thriller)
        print("Horror: ",this_user.Horror)
        print("Drama: ",this_user.Drama)
        print("Romance: ",this_user.Romance)
        print("SciFi: ", this_user.SciFi)
        this_user.save()
        return redirect('/watchlist')
    else:
        return redirect('/reg/')

def add_review(request, id):
    if 'curuser' in request.session:
        errors = Review.objects.revvalidate(request.POST)
        if len(errors):
            for error in errors:
                messages.error(request, error)
        return redirect ('/'+id)
    else:
        return redirect('/reg/')

def rm_review(request, id, rev):
    if 'curuser' in request.session:
        Review.objects.get(id=rev).delete()
        return redirect(('/'+id))
    else:
        return redirect('/reg/')

def result(request, search_option):
    title = request.POST['search'].replace(' ', '+')
    url = "https://api.themoviedb.org/3/search/"+search_option+"?api_key=1a1ef1aa4b51f19d38e4a7cb134a5699&query="+title+"&page=1"
    strresponse = requests.get(url).content
    response = json.loads(strresponse)
    context = {
        "response" : response,
    }
    return render(request, 'imdb/_results.html', context)
