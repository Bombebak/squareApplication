# Squares
Interdisciplinary mandatory assignment 

<p>At the end of our first semester we were tasked with a group assignment to create a prototype for a company called KongOrange. 
Here is the description of the case:</p>
<p>
"Squares is a digital gallery for new art, where gallery visitors can fashion and create the final
expression of the art pieces.</p>
<p>
On Squares artists create new artworks that are divided into small digital pieces that visitors can play
around with and put together via a digital interface. Squares is an open art playground where you
can purchase printed versions of the artists' works reworked into your particular personal design.</p><p>
Squares will be a global, user-driven and interactive gallery where people from around the globe can
dive into the artworks and create works that fit their expression and experience.
At the same time Squares is a fascinating and imaginative business concept that attracts artists,
both because, of the initial challenge to their creativity, and because they can sell and profit from
their work."
</p>

<h3>Design</h3>
<p>
Since the project was called squares we decided to enforce through consistent design the idea that everything should be squares.
People are creating squares; the designers are uploading square shaped tiles, 
so why not build the website around the concept of squares? 
We implemented the idea by sizing every element on the page in multiples of 100px. 
We did our best to make everything on the website be a multiple of a 100px square or at least the multiple of the logo. 
This particular items exactly 500px and is 1000px wide. Of course, 
these dimensions change since the website will be responsive- but at least we can produce a nice experience for users on desktop mode. 
</p>
<p>
We are also designing in the magic number 5. This is done by for example having a big square with four tiles inside 
which makes it five squares. The tile in the bottom right corner will not be like the other tiles. 
An example of this is in the browsing page where the collections is, and in the bottom left, top left and 
right tiles will show the image of themselves and the bottom right will show the name of the artist and the title of the collection.  
</p>
<p>Image of browsing gallery</p>

<h3>Frontend</h3>
<p>
We used Sass to to style the website which is a fantastic tool. Since we aimed for making everything sqaured we had to
create mixins in order for it to be responsive otherwise the sizes of the squares would be either too big or small for the device.
</p>
<p>
When an user clicked on a navigational link the color of the background would change depending on the color of the clicked link 
and Sass helped us make this possible.
</p>
