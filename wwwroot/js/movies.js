const deleteMovie = async (id) => {

    console.log('deleteMovie', id);
    try{
        await fetch(`https://localhost:7290/admin/Delmovie?movieID=${id}`, {
            method: 'POST',
        });
    }
    catch(err){
        console.error('Error:', err);
    }
};

